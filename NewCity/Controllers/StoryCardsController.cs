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
        private readonly NewCItyDbContext _context;

        public StoryCardsController(NewCItyDbContext context)
        {
            _context = context;
        }

        // GET: StoryCards
        public async Task<IActionResult> Index()
        {
            return View(await _context.StoryCard.ToListAsync());
        }

        // GET: StoryCards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyCard = await _context.StoryCard.Include(s => s.StoryOptions)
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.ID == id);
               
            if (storyCard == null)
            {
                return NotFound();
            }

            return View(storyCard);
        }

        // GET: StoryCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoryCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StorySeriesID,StoryName,Text,IMG,BackgroundIMG")] StoryCard storyCard)
        {
            if (ModelState.IsValid)
            {
                storyCard.ID = Guid.NewGuid();
                _context.Add(storyCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storyCard);
        }

        // GET: StoryCards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyCard = await _context.StoryCard.FindAsync(id);
            if (storyCard == null)
            {
                return NotFound();
            }
            return View(storyCard);
        }

        // POST: StoryCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,StorySeriesID,StoryName,Text,IMG,BackgroundIMG")] StoryCard storyCard)
        {
            if (id != storyCard.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try 
                {
                    _context.Update(storyCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoryCardExists(storyCard.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(storyCard);
        }

        // GET: StoryCards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyCard = await _context.StoryCard
                .FirstOrDefaultAsync(m => m.ID == id);
            if (storyCard == null)
            {
                return NotFound();
            }

            return View(storyCard);
        }

        // POST: StoryCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var storyCard = await _context.StoryCard.FindAsync(id);
            _context.StoryCard.Remove(storyCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoryCardExists(Guid id)
        {
            return _context.StoryCard.Any(e => e.ID == id);
        }
    }
}
