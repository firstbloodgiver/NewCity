﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using NewCity.Data;
using NewCity.Models;
using Newtonsoft.Json;

namespace NewCity.Controllers
{
    public class CreateStoryController : BaseController
    {
        public CreateStoryController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context)
            : base(SignInManager, UserManager, context)
        {
        }

        public IActionResult Index(string id)
        {
            List<StoryCard> storyCards = _context.StoryCard.Where(a => a.StorySeriesID == Guid.Parse(id)).Include(a => a.StoryOptions).ToList();
            if (storyCards.Count == 0) {
                StoryCard card = new StoryCard() {
                    ID = Guid.NewGuid(),
                    StorySeriesID = Guid.Parse(id),
                    StoryName = string.Empty,
                };
                storyCards.Add(card);
                _context.StoryCard.Add(card);
                _context.SaveChanges();
            }
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
        /// 保存故事卡
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([Bind("ID,Text,IMG,BackgroundIMG,StoryOptions")]StoryCard storyCard)
        {
            //如果你是该故事系列的作者才可以保存

            StoryCard card = _context.StoryCard.Where(a => a.ID == storyCard.ID).Include(i => i.StoryOptions).FirstOrDefault();

            StorySeries series = _context.StorySeries.Where(a => a.ID == card.StorySeriesID).AsNoTracking().FirstOrDefault();
            var userid = GetUserId();
            if (series.Author == userid)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        card.Text = storyCard.Text;
                        card.IMG = storyCard.IMG;
                        if (storyCard.StoryOptions != null)
                        {
                            if (storyCard.StoryOptions.Count() != card.StoryOptions.Count())
                            {
                                //存在选项更改
                                card.StoryOptions = null;
                                var deletelist = _context.StoryOption.Where(a => a.StoryCardID == card.ID).ToList();
                                _context.StoryOption.RemoveRange(deletelist);
                                foreach (var option in storyCard.StoryOptions)
                                {
                                    option.StoryCardID = card.ID;
                                    option.Condition = storyCard.StoryOptions.Where(a => a.ID == option.ID).First().Condition;
                                }
                                _context.StoryOption.AddRange(storyCard.StoryOptions);
                                _context.SaveChanges();
                            }
                            else
                            {
                                foreach (var option in card.StoryOptions)
                                {
                                    option.Condition = storyCard.StoryOptions.Where(a => a.ID == option.ID).First().Condition;

                                }
                            }
                        }
                        else
                        {
                            card.StoryOptions = null;
                            _context.SaveChanges();
                        }

                        _context.StoryCard.Update(card);
                        await _context.SaveChangesAsync();
                        return new JsonResult(true);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            else
            {
                return new JsonResult(true);
            }


            return new JsonResult(true);
        }

    }

}

        