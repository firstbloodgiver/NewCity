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
            if (AccoundID == Guid.Empty.ToString())
            {
                return RedirectToAction("PleaseLogin", "Home");
            }
            if (_SignInManager.IsSignedIn(User)|| AccoundID != null)
            {
                List<StorySeries> storySeries = new List<StorySeries>();
                storySeries = _context.StorySeries.Where(a => a.Author == Guid.Parse( AccoundID) && a.IsCancel != true).ToList();
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
                storySeries.IsCancel = true;
                await _context.SaveChangesAsync();
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> Active(string id)
        {
            var storySeries = await _context.StorySeries.FirstOrDefaultAsync(m => m.ID == Guid.Parse(id));
            if (storySeries.Author == GetUserId() && Integrity(storySeries.ID).Count() == 0)
            {
                storySeries.Status = Enum.enumStoryStatus.进行中;
                await _context.SaveChangesAsync();
                return Json(true);
            }
            else if(Integrity(storySeries.ID).Count() > 0)
            {
                return Json(Integrity(storySeries.ID));
            }
            return Json(false);
        }

        /// <summary>
        /// 完整性检测
        /// </summary>
        private IEnumerable<Guid> Integrity(Guid storySeriesID)
        {
            List<Guid> cardIDs = new List<Guid>();

            var temp = _context.StoryOption.AsNoTracking().Where(a => a.NextStoryCardID == Guid.Empty && a.Effect.Contains("结束故事") != true).ToList();
            foreach(var option in temp)
            {
                cardIDs.Add(option.StoryCardID);
            }
            var result = cardIDs.Distinct();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AntiActive(string id)
        {
            var storySeries = await _context.StorySeries.FirstOrDefaultAsync(m => m.ID == Guid.Parse(id));
            if (storySeries.Author == GetUserId())
            {
                storySeries.Status = Enum.enumStoryStatus.测试;
                await _context.SaveChangesAsync();
                return Json(true);
            }
            return Json(false);
        }
    }
}