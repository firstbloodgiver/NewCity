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

        public async Task<IActionResult> Index()
        {
            var userid = Guid.Parse(_userManager.GetUserId(User));
            var list = await _context.UserCharacter.Where(p => p.UserId == userid)
                .Join(_context.CharacterSchedule, u => u.ID, c => c.CharacterID, (u, c) => new { CNStorySeriesID = c.StorySeriesID }).ToArrayAsync();


            return View(list);
        }

        //public async Task<IActionResult> Index(Guid? StorySeriesID)
        //{
        //    // 读取出该ID的故事卡片
        //    var card = await _context
        //        .StoryCard
        //        .Include(s => s.StoryOptions)
        //        .FirstOrDefaultAsync(m => m.StorySeriesID == StorySeriesID);



        //    return View(card);
        //}


        [HttpPost]
        public async Task<JsonResult> NextCart(Guid? storyID)
        {
            //TODO 添加反作弊
            var card = await _context
               .StoryCard
                .Include(s => s.StoryOptions)
                .FirstOrDefaultAsync(m => m.ID == storyID);

            
            return Json(card);
        }


    }
}