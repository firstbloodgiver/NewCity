using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewCity.Models;
using Microsoft.AspNetCore.Identity;
using NewCity.Data;
using NewCity.Enum;
using Microsoft.EntityFrameworkCore;

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
            ViewBag.Silde = _context.HomeNews.Where(a => a.Type == (int)HomeType.轮播).OrderBy(a => a.CreateTime).Take(3).ToList();
            ViewBag.Content = _context.HomeNews.Where(a => a.Type == (int)HomeType.内容).OrderBy(a => a.CreateTime).Take(4).ToList();
            ViewBag.Publicity = _context.HomeNews.Where(a => a.Type == (int)HomeType.主体语).OrderBy(a => a.CreateTime).FirstOrDefault();

            return View();
        }


        public IActionResult PleaseLogin() {
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


        public IActionResult List()
        {
            Guid userid = GetUserId();
            if (userid != Guid.Empty)
            {
                var creator = _context.Creator.AsNoTracking().Where(a => a.UserID == userid).FirstOrDefault();
                if (creator != null)
                {
                    return RedirectToAction("Index", "Creator");
                }
                else
                {
                    return RedirectToAction("StorySelect", "Main");
                }
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
