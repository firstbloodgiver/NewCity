using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewCity.Data;
using NewCity.Models;
using Newtonsoft.Json;

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
            
            return View(storyCards);
        }

        /// <summary>
        /// 返回故事卡信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetCard(string ID)
        {
            
            StoryCard storyCard = _context.StoryCard.Where(a => a.ID == Guid.Parse(ID)).FirstOrDefault();
            return JsonConvert.SerializeObject(storyCard);
        }

        


    }
}