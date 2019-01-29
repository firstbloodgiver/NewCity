using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            StoryCard card = _context.StoryCard.Where(a => a.ID == storyCard.ID).AsNoTracking().FirstOrDefault();
            StorySeries series = _context.StorySeries.Where(a => a.ID == card.StorySeriesID).AsNoTracking().FirstOrDefault();
            var userid = GetUserId();
            if (series.Author == userid)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        storyCard.StorySeriesID = card.StorySeriesID;

                        _context.Update(storyCard);
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

        