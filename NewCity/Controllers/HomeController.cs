using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewCity.Models;
using Microsoft.AspNetCore.Identity;
using NewCity.Data;

namespace NewCity.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context)
           : base(SignInManager, UserManager, context)
        {
        }


        public IActionResult Index()
        {
            
            if (_SignInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }




        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
