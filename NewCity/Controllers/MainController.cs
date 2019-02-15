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
    public class MainController : BaseController
    {
        public MainController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context)
            : base(SignInManager, UserManager, context)
        {
        }

        public IActionResult Index()
        {
            var userid = GetUserId();

            Guid StorySeriesID = Guid.Empty;
            
            List<StoryCard> OperaList = new List<StoryCard>();


            //是否在场景
            if (InLocation(userid, out StorySeriesID))
            {
                StorySeries ReadList = new StorySeries();
                ReadList = _context.StorySeries.AsNoTracking().Where(s => s.ID == StorySeriesID).FirstOrDefault();
                OperaList = _context.StoryCard.AsNoTracking().Where(s => s.StorySeriesID == StorySeriesID).ToList();
                ViewBag.ReadList = ReadList;
                ViewBag.OperaList = OperaList;
            }
            else
            {
                StoryCard ReadList = new StoryCard();
                var storycardID = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == userid)
                    .Join(_context.CharacterSchedule, a => a.ID, b => b.CharacterID, (a, b) => new { storycardID = b.StoryCardID,b.IsMain })
                    .FirstOrDefault(b=>b.IsMain==true);
                ReadList = _context.StoryCard.AsNoTracking().Include(a=>a.StoryOptions).AsNoTracking().SingleOrDefault(a => a.ID == storycardID.storycardID);
                //去除不符合显示条件的选项
                List<StoryOption> storyOptions = new List<StoryOption>();
                foreach (var option in ReadList.StoryOptions) {
                    if (check(option.Condition)) {
                        storyOptions.Add(option);
                    }
                }
                ReadList.StoryOptions = storyOptions;
                ViewBag.ReadList = ReadList;
            }
            

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> NextCart(Guid? storyID)
        {
            var userid = GetUserId();
            var Schedule = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == userid)
                .Join(_context.CharacterSchedule, a => a.ID,b=>b.CharacterID,(a,b)=>new { IsStory = b.IsStory, StorySeriesID=b.StorySeriesID, StoryCardID =b.StoryCardID })
                .FirstOrDefault();
            if (Schedule.IsStory) {
                List<StoryOption> Options = new List<StoryOption>();
                var storycards = _context.StoryCard.AsNoTracking().Where(a => a.ID == Schedule.StoryCardID).Include(a=>a.StoryOptions).FirstOrDefault();
                foreach (var option in storycards.StoryOptions) {
                    if (check(option.Condition)) {
                        Options.Add(option);
                    }
                }
                if (Options.Where(a => a.NextStoryCardID == storyID).FirstOrDefault() == null) {
                    return Json("不存在的后续故事卡片！");
                }
            }
            else
            {
                //地图场景处理
            }
            var card = await _context
               .StoryCard
               .AsNoTracking()
               .Include(s => s.StoryOptions)
               .FirstOrDefaultAsync(m => m.ID == storyID);

            return Json(card);
        }

        /// <summary>
        /// 检查是否符合显示条件
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        private bool check(string Condition)
        {
            
            return true;
        }

        private bool InLocation(Guid userid, out Guid StorySeriesID)
        {
            var location = _context.UserCharacter.AsNoTracking().Where(p => p.UserId == userid)
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