using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewCity.Data;
using NewCity.Models;

namespace NewCity.Controllers
{
    public class StoryCardsController : Controller
    {
        private readonly NewCityDbContext _context;

        public StoryCardsController(NewCityDbContext context)
        {
            _context = context;
        }

        // GET: StoryCards
        public async Task<IActionResult> Index()
        {
            var card = await _context.StoryCard
                .Include(s => s.StoryOptions)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.ID = card[0].StorySeriesID;
            return View(card);
        }

        
    }
}
