using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewCity.Data;
using NewCity.Models;

namespace NewCity.Controllers
{
    public class CreatorController : Controller
    {
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly NewCityDbContext _context;


        public CreatorController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context ) {
            _SignInManager = SignInManager;
            _userManager = UserManager;
            _context = context;
            
        }

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
            string AccoundID = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                storySeries.ID = Guid.NewGuid();
                storySeries.Author = Guid.Parse(AccoundID);
                _context.Add(storySeries);
                await _context.SaveChangesAsync();
            }
            
            return Index();
        }

        [HttpDelete]

    }
}