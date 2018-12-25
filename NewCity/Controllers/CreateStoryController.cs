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
    public class CreateStoryController : Controller
    {

        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly NewCityDbContext _context;

        public CreateStoryController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context) {
            _SignInManager = SignInManager;
            _userManager = UserManager;
            _context = context;
        }

        public IActionResult Index(string storySeriID)
        {
            List<StoryCard> storyCards = _context.StoryCard.Where(a => a.StorySeriesID == Guid.Parse(storySeriID)).ToList();
            //在列表中按照下一项排序
            return View();
        }
    }
}