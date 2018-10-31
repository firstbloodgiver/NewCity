using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewCity.Data;

namespace NewCity.Controllers
{
    public class MainController : Controller
    {
        private readonly NewCItyDbContext _context;

        public MainController(NewCItyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? storyID)
        {
            //TODO 读取出该ID的故事卡片
            var card = await _context.StoryCard
                .Include(s => s.StoryOptions)
                .AsNoTracking()
                .ToListAsync();


            return View();
        }
    }
}