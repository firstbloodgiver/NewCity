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
    public class StorySeriesController : Controller
    {
        private readonly NewCityDbContext _context;

        public StorySeriesController(NewCityDbContext context)
        {
            _context = context;
        }

        // GET: StorySeries
        public async Task<IActionResult> Index()
        {
            return View(await _context.StorySeries.ToListAsync());
        }

        // GET: StorySeries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storySeries = await _context.StorySeries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (storySeries == null)
            {
                return NotFound();
            }

            return View(storySeries);
        }

        // GET: StorySeries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StorySeries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SeriesName,Author,Creationdate")] StorySeries storySeries)
        {
            if (ModelState.IsValid)
            {
                storySeries.ID = Guid.NewGuid();
                _context.Add(storySeries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storySeries);
        }

        // GET: StorySeries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storySeries = await _context.StorySeries.FindAsync(id);
            if (storySeries == null)
            {
                return NotFound();
            }
            return View(storySeries);
        }

        // POST: StorySeries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,SeriesName,Author,Creationdate")] StorySeries storySeries)
        {
            if (id != storySeries.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storySeries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorySeriesExists(storySeries.ID))
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
            return View(storySeries);
        }

        // GET: StorySeries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storySeries = await _context.StorySeries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (storySeries == null)
            {
                return NotFound();
            }

            return View(storySeries);
        }

        // POST: StorySeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var storySeries = await _context.StorySeries.FindAsync(id);
            _context.StorySeries.Remove(storySeries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorySeriesExists(Guid id)
        {
            return _context.StorySeries.Any(e => e.ID == id);
        }
    }
}
