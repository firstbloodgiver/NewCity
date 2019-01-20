﻿using System;
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

        public JsonResult SaveCondition(List<StoryStatus> storyStates,string optionID) {
            if(!storyStates.Any(a=>a.StorySeries == storyStates.FirstOrDefault().StorySeries)){
                return new JsonResult(0);
            }
            var Author = _context.StorySeries.Where(a => a.ID == Guid.Parse(storyStates.FirstOrDefault().StorySeries)).First().Author;
            if (_userManager.GetUserId(User) != Author.ToString()) {
                return new JsonResult(0);
            }
            

        }


        

    }
}