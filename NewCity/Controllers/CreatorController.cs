using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewCity.Data;
using NewCity.Models;

namespace NewCity.Controllers
{
    public class CreatorController : BaseController
    { 

        public CreatorController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context)
            : base(SignInManager, UserManager, context){}

        public IActionResult Index()
        {

            string AccoundID = _userManager.GetUserId(User);
            if (_SignInManager.IsSignedIn(User)|| AccoundID != null)
            {
                List<StorySeries> storySeries = new List<StorySeries>();
                storySeries = _context.StorySeries.Where(a => a.Author == Guid.Parse( AccoundID)).ToList();

                ViewBag.storySeries = storySeries;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("SeriesName,Text")] StorySeries storySeries)
        {
            string AccoundID = GetUserId().ToString();
            if (ModelState.IsValid)
            {
                storySeries.ID = Guid.NewGuid();
                storySeries.Author = Guid.Parse(AccoundID);
                _context.Add(storySeries);
                await _context.SaveChangesAsync();
            }
            
            return Index();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            var storySeries = await _context.StorySeries.FirstOrDefaultAsync(m => m.ID == Guid.Parse(id));
            if (storySeries.Author == GetUserId()) {
                _context.StorySeries.Remove(storySeries);
                await _context.SaveChangesAsync();
                return new JsonResult(true);
            }
            return new JsonResult(false);
        }

    }
}