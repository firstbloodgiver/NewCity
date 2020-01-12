using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewCity.Data;
using NewCity.Enum;
using NewCity.Models;

namespace NewCity.Controllers
{
    public class ReviewController : BaseController
    {
        public ReviewController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context)
            : base(SignInManager, UserManager, context) { }
        public IActionResult Review()
        {
            string AccoundID = _userManager.GetUserId(User);
            if (AccoundID == Guid.Empty.ToString())
            {
                return RedirectToAction("PleaseLogin", "Home");
            }
            if (_SignInManager.IsSignedIn(User) || AccoundID != null)
            {
                List<StorySeries> storySeries = new List<StorySeries>();
                storySeries = _context.StorySeries.Where(a => a.IsCancel != true && a.Status == enumStoryStatus.审批中).ToList();
                ViewBag.storySeries = storySeries;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Active(string id)
        {
            try
            {
                var storySeries = await _context.StorySeries.FirstOrDefaultAsync(m => m.ID == Guid.Parse(id));

                storySeries.ReviewContent = "通过审核 " + DateTime.Now.ToString();
                storySeries.Status = Enum.enumStoryStatus.进行中;
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AntiActive(string id, string content)
        {
            try
            {
                var storySeries = await _context.StorySeries.FirstOrDefaultAsync(m => m.ID == Guid.Parse(id));

                storySeries.ReviewContent = content;
                storySeries.Status = Enum.enumStoryStatus.测试;
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }

        }
    }
}