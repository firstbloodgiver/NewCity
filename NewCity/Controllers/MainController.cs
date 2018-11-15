using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewCity.Data;
using System.Web;
using Newtonsoft.Json;


namespace NewCity.Controllers
{
    public class MainController : Controller
    {
        private readonly NewCityDbContext _context;

        public MainController(NewCityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(Guid? StorySeriesID)
        {
            // 读取出该ID的故事卡片
            var card = await _context
                .StoryCard
                .Include(s => s.StoryOptions)
                .FirstOrDefaultAsync(m => m.StorySeriesID == StorySeriesID);



            return View(card);
        }


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