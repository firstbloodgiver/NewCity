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

        public IActionResult Index(string id)
        {
            List<StoryCard> storyCards = _context.StoryCard.Where(a => a.StorySeriesID == Guid.Parse(id)).ToList();
            
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

       
        /// <summary>
        /// 获取模态框默认条件
        /// </summary>
        /// 
        public string GetCondition(string optionid) {
            StoryOption storyOptions = _context.StoryOption.Where(a => a.ID == Guid.Parse(optionid)).FirstOrDefault();
            if (string.IsNullOrEmpty(storyOptions.Condition))
            {
                return string.Empty;
            }
            return storyOptions.Condition;


        }


        /// <summary>
        /// 保存故事卡
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> Save(StoryCard storyCard) {
            //如果你是该故事系列的作者才可以保存
            if (ModelState.IsValid) {
                _context.Add(storyCard);
                await _context.SaveChangesAsync();
            }

            return new JsonResult(true);
        }

    }
}