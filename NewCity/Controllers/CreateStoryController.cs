using System;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">修改的故事系列</param>
        /// <returns></returns>
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
        [ValidateAntiForgeryToken]
        public JsonResult Save(string json)
        {
            var ResultJson = new JsonResult(false);
            if (!string.IsNullOrEmpty(json))
            {
                StoryCard card = JsonConvert.DeserializeObject<StoryCard>(json);
                StoryCard storyCard = _context.StoryCard.Include(a => a.StoryOptions).Where(a => a.ID == card.ID).SingleOrDefault();
                storyCard.Text = card.Text;
                storyCard.IMG = card.IMG;
                storyCard.BackgroundIMG = card.BackgroundIMG;
                storyCard.StoryOptions = card.StoryOptions;
                _context.StoryCard.Update(storyCard);
                _context.SaveChanges();
                ResultJson.Value = true;
            }
            return ResultJson;
        }



        /// <summary>
        /// 移动到下一卡片，若id无则新建
        /// </summary>
        /// <param name="id">指定的下一卡片id</param>
        /// <param name="seriesid">故事系列id</param>
        /// <param name="optionid">选项id</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NextCard(Guid nextid, Guid seriesid,Guid optionid) {
            int statuscode = 0;
            string ContentType = string.Empty;
            if (nextid != Guid.Empty ) {
                var card = _context.StoryCard.Where(a => a.ID == nextid).AsNoTracking().FirstOrDefault();
                if (card != null ) {
                    if (_context.StorySeries.Where(a => a.ID == card.StorySeriesID).AsNoTracking().FirstOrDefault().Author == GetUserId()) {
                        var option = _context.StoryOption.Where(a => a.ID == optionid).FirstOrDefault();
                        if (option.NextStoryCardID != nextid) {
                            //if (_context.StoryOption.Where(a => a.NextStoryCardID == nextid).AsNoTracking().ToArray().Length == 0) {
                            //删除
                            //}
                            option.NextStoryCardID = nextid;
                            _context.StoryOption.Update(option);
                            _context.SaveChanges();
                            return Json(_context.StoryCard.AsNoTracking().Where(a => a.ID == nextid).FirstOrDefault());

                        }

                        StoryCard result1 = _context.StoryCard.AsNoTracking().Where(a => a.ID == nextid).Include(a=>a.StoryOptions).FirstOrDefault();
                        return Json(result1);
                    }
                    else
                    {
                        statuscode = 3;
                        ContentType = "权限不足！";
                    }
                }
                else
                {
                    statuscode = 2;
                    ContentType = "不存在该卡片！";
                }

            }
            else
            {
                var option = _context.StoryOption.Where(a => a.ID == optionid).FirstOrDefault();
                //新建卡片
                StoryCard storyCard = new StoryCard()
                {
                    ID = Guid.NewGuid(),
                    StorySeriesID  = seriesid,                    
                };
                option.NextStoryCardID = storyCard.ID;
                _context.StoryOption.Update(option);
                _context.StoryCard.Add(storyCard);
                _context.SaveChanges();

                return Json(storyCard);
            }
            JsonResult result = new JsonResult(false) {
                StatusCode = statuscode,
                ContentType = ContentType,
            };
            
            return Json(result);
        }
    }

}

        