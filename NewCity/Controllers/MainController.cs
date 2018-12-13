using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NewCity.Data;
using NewCity.Models;
using System.Web;
using Newtonsoft.Json;


namespace NewCity.Controllers
{
    public class MainController : Controller
    {
        private readonly NewCityDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MainController(NewCityDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }

        public IActionResult Index()
        {
            var userid = Guid.Parse(_userManager.GetUserId(User));

            Guid StorySeriesID = Guid.Empty;
            
            List<StoryCard> OperaList = new List<StoryCard>();


            //是否在场景
            if (InLocation(userid, out StorySeriesID))
            {
                StorySeries ReadList = new StorySeries();
                ReadList = _context.StorySeries.Where(s => s.ID == StorySeriesID).FirstOrDefault();
                OperaList = _context.StoryCard.Where(s => s.StorySeriesID == StorySeriesID).Where(s =>check(s.flag)==true).ToList();
                ViewBag.ReadList = ReadList;
                ViewBag.OperaList = OperaList;
            }
            else
            {
                StoryCard ReadList = new StoryCard();
                var storycardID = _context.UserCharacter.Where(a => a.UserId == userid)
                    .Join(_context.CharacterSchedule, a => a.ID, b => b.CharacterID, (a, b) => new { storycardID = b.StoryCardID,b.IsMain }).FirstOrDefault(b=>b.IsMain==true);
                ReadList = _context.StoryCard.Include(a=>a.StoryOptions).AsNoTracking().SingleOrDefault(a => a.ID == storycardID.storycardID);
                ViewBag.ReadList = ReadList;
            }
            

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> NextCart(Guid? storyID)
        {

            var card = await _context
               .StoryCard
                .Include(s => s.StoryOptions)
                .FirstOrDefaultAsync(m => m.ID == storyID);

            
            return Json(card);
        }

        /// <summary>
        /// 检查是否符合显示条件
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private bool check(string a)
        {
            return false;
        }

        private bool InLocation(Guid userid, out Guid StorySeriesID)
        {
            var location = _context.UserCharacter.Where(p => p.UserId == userid)
                .Join(_context.CharacterSchedule,u => u.ID,c => c.CharacterID,(u,c)=>new { c.IsStory,c.StorySeriesID }).ToList();

            StorySeriesID = Guid.Empty;

            foreach (var state in location)
            {
                if(state.IsStory == true)
                {
                    StorySeriesID = state.StorySeriesID;
                    ViewBag.InLocation = false;
                    return false;
                }
            }
            ViewBag.InLocation = true;
            return true;
        }
        



    }

}