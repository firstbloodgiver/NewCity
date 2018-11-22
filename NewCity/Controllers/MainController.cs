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
            var list =  _context.UserCharacter.Where(p => p.UserId == userid)
                .Join(_context.CharacterSchedule, u => u.ID, c => c.CharacterID, (u, c) => new { SeriesID = c.StorySeriesID ,c.StoryCardID}).ToList()
                .Join(_context.StorySeries, a => a.SeriesID, b => b.ID, (a, b) => new { StorySeriesName = b.SeriesName, StorySeriesID =b.ID , NextStoryCard = a.StoryCardID}).ToList();

            List<Mainlist> mainlists = new List<Mainlist>();

            foreach(var item in list)
            {
                Mainlist mainlist = new Mainlist();
                mainlist.StorySeriesID = item.StorySeriesID;
                mainlist.NextStoryCard = item.NextStoryCard;
                mainlist.StorySeriesName = item.StorySeriesName;
                mainlists.Add(mainlist);
            }

            ViewBag.list=mainlists;

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

        public class Mainlist
        {
            public string StorySeriesName = string.Empty;
            public Guid StorySeriesID;
            public Guid NextStoryCard;
        }


    }

}