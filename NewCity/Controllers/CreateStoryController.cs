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
            try
            {
                var Creatorschedule = _context.CreatorSchedule.FirstOrDefault(a => a.UserID == GetUserId() && a.StorySeriesID == Guid.Parse(id));
                if (Creatorschedule != null)
                {
                    Guid lastStoryCardID = Creatorschedule.StoryCardID;
                    ViewBag.LastCard = _context.StoryCard.AsNoTracking().FirstOrDefault(a => a.ID == lastStoryCardID);
                }
                else
                {
                    StoryCard card = new StoryCard()
                    {
                        ID = Guid.NewGuid(),
                        StorySeriesID = Guid.Parse(id),
                        StoryName = string.Empty,

                    };

                    CreatorSchedule creatorSchedule = new CreatorSchedule()
                    {
                        ID = Guid.NewGuid(),
                        UserID = GetUserId(),
                        StorySeriesID = Guid.Parse(id),
                        StoryCardID = card.ID
                    };

                    _context.StoryCard.Add(card);
                    _context.SaveChanges();
                    ViewBag.LastCard = card;
                }

                List<StoryCard> storyCards = _context.StoryCard.AsNoTracking().Where(a => a.StorySeriesID == Guid.Parse(id)).Include(a => a.StoryOptions).ToList();
                return View(storyCards);
            }
            catch
            {
                return NotFound();
            }
            
        }

        
        /// <summary>
        /// 返回故事卡信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetCard(string ID)
        {
            StoryCard storyCard = _context.StoryCard.AsNoTracking().FirstOrDefault(a => a.ID == Guid.Parse(ID));
            return JsonConvert.SerializeObject(storyCard);
        }

        /// <summary>
        /// 保存故事卡
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(string json)
        {
            
            if (!string.IsNullOrEmpty(json))
            {
                StoryCard card = JsonConvert.DeserializeObject<StoryCard>(json);
                StoryCard storyCard = _context.StoryCard.Include(a => a.StoryOptions).FirstOrDefault(a=>a.ID == card.ID);
                storyCard.Text = card.Text;
                storyCard.IMG = card.IMG;
                storyCard.BackgroundIMG = card.BackgroundIMG;
                //storyCard.StoryOptions = card.StoryOptions;

                foreach (var obj in card.StoryOptions)
                {
                    var temp = storyCard.StoryOptions.FirstOrDefault(a => a.ID == obj.ID);
                    if(temp == null)
                    {
                        StoryOption option = new StoryOption()
                        {
                            ID = obj.ID,
                            StoryCardID = obj.StoryCardID,
                            Text = obj.Text,
                            NextStoryCardID = obj.NextStoryCardID,
                            Condition = obj.Condition,
                            Effect = obj.Effect
                        };
                        storyCard.StoryOptions.Add(option);
                    }
                    else
                    {
                        temp.Condition = obj.Condition;
                        temp.Effect = obj.Effect;
                        temp.Text = obj.Text;
                    }    
                    
                }
                await _context.SaveChangesAsync();
                //return Index(card.StorySeriesID.ToString());
                return RedirectToAction(nameof(Index),new { id = card.StorySeriesID.ToString() });
            }
            return NotFound();

        }



        /// <summary>
        /// 移动到下一卡片，若id无则新建
        /// </summary>
        /// <param name="id">指定的下一卡片id</param>
        /// <param name="seriesid">故事系列id</param>
        /// <param name="optionid">选项id</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NextCard(Guid Id)
        {
            Guid nextCardId;
            try
            {
                StoryOption storyOption = _context.StoryOption.FirstOrDefault(a => a.ID == Id);
                StoryCard storyCard = _context.StoryCard.FirstOrDefault(a => a.ID == storyOption.StoryCardID);
                if (string.IsNullOrEmpty(storyOption.NextStoryCardID.ToString()) || storyOption.NextStoryCardID == Guid.Empty)
                {
                    

                    StoryCard NewStoryCard = new StoryCard()
                    {
                        ID = Guid.NewGuid(),
                        StorySeriesID = storyCard.StorySeriesID,
                        StoryName = string.Empty,
                    };
                    _context.StoryCard.Add(NewStoryCard);
                    

                    nextCardId = NewStoryCard.ID;
                }
                else
                {
                    nextCardId = storyOption.NextStoryCardID;

                }
                _context.CreatorSchedule.FirstOrDefault(a => a.ID == GetUserId() && a.StorySeriesID == storyCard.StorySeriesID).StoryCardID = nextCardId;
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), new { id = nextCardId });
            }
            catch
            {
                return NotFound();
            }
        }
    }

}

        