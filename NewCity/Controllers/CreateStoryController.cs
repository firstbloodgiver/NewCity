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
        /// 根据故事卡片Id返回
        /// </summary>
        /// <param name="id">故事卡片</param>
        /// <returns></returns>
        public IActionResult Index(string id)             
        {
            try
            {
                var Creatorschedule = _context.CreatorSchedule.FirstOrDefault(a => a.UserID == GetUserId() && a.StorySeriesID == Guid.Parse(id));
                if (Creatorschedule != null)
                {
                    Guid lastStoryCardID = Creatorschedule.StoryCardID;
                    ViewBag.LastCard = _context.StoryCard.AsNoTracking().Include(a=>a.StoryOptions).FirstOrDefault(a => a.ID == lastStoryCardID);
                }
                else
                {
                    StoryCard card = new StoryCard()
                    {
                        ID = Guid.NewGuid(),
                        StorySeriesID = Guid.Parse(id),
                        StoryName = string.Empty,
                        IsHead = true
                    };

                    CreatorSchedule creatorSchedule = new CreatorSchedule()
                    {
                        ID = Guid.NewGuid(),
                        UserID = GetUserId(),
                        StorySeriesID = Guid.Parse(id),
                        StoryCardID = card.ID
                    };
                    _context.CreatorSchedule.Add(creatorSchedule);
                    _context.StoryCard.Add(card);
                    
                    ViewBag.LastCard = card;
                }
                _context.SaveChanges();
                List<StoryCard> storyCards = _context.StoryCard.AsNoTracking().Where(a => a.StorySeriesID == Guid.Parse(id)).Include(a => a.StoryOptions).ToList();
                return View(storyCards);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }

        /// <summary>
        /// 故事卡树
        /// </summary>
        List<StoryCardTree> storyCardTrees = new List<StoryCardTree>();
        List<StoryCard> storyCards = new List<StoryCard>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">StorySeriesID</param>
        /// <returns></returns>
        public IActionResult FlowChart(string id)
        {
            List<StoryCardTree> StoryCardTrees = new List<StoryCardTree>();
            var obj = _context.StoryCard.AsNoTracking().Where(a => a.StorySeriesID == Guid.Parse(id)).Include(a => a.StoryOptions).ToList().OrderBy(a => a.IsHead);
            if(obj.Count() == 0)
            {
                return RedirectToAction(nameof(Index), new { id });
            }
            foreach (var temp in obj)
            {
                StoryCard storyCard = new StoryCard()
                {
                    ID = temp.ID,
                    StorySeriesID = temp.StorySeriesID,
                    StoryName = temp.StoryName,
                    Text = temp.Text,
                    IMG = temp.IMG,
                    BackgroundIMG = temp.BackgroundIMG,
                    IsHead = temp.IsHead,
                    StoryOptions = temp.StoryOptions,
                };
                storyCards.Add(storyCard);
            }
            StoryCard FirstStoryCard = storyCards.Where(a => a.IsHead == true).FirstOrDefault();
            if (FirstStoryCard != null)
            {
                CreateTree(FirstStoryCard,Guid.Empty, 0);
            }
            var tree = storyCardTrees.OrderBy(a => a.Level).GroupBy(a => a.Level).ToList();
            List<StoryCardTrees> Trees = new List<StoryCardTrees>();
            foreach (var i in tree)
            {
                StoryCardTrees cardTrees = new StoryCardTrees()
                {
                    StoryCard = storyCardTrees.Where(a => a.Level == i.Key).ToList(),
                    Level = i.Key
                };

                Trees.Add(cardTrees);
            }

            ViewBag.StoryCardTree = Trees;
            return View();
        }

        /// <summary>
        /// 递归添加树元素
        /// </summary>
        /// <param name="storyCard"></param>
        /// <param name="level"></param>
        private void CreateTree(StoryCard storyCard,Guid fatherStoryCardId,int level)
        {
            StoryCardTree storyCardTree = new StoryCardTree()
            {
                FatherStoryCardId = fatherStoryCardId,
                Level = level,
                StoryCard = storyCard
            };
            storyCardTrees.Add(storyCardTree);
            foreach (var option in storyCard.StoryOptions)
            {
                StoryCard ChildStoryCard = storyCards.Where(a => a.ID == option.NextStoryCardID).First();
                if (ChildStoryCard != null)
                {
                    CreateTree(ChildStoryCard, storyCard.ID, level + 1);
                }
            }
        }

        /// <summary>
        /// 故事卡片返回到树
        /// </summary>
        /// <param name="StorySeriesID"></param>
        public IActionResult ReturnTree(string StorySeriesID)
        {
            CreatorSchedule creatorSchedule = _context.CreatorSchedule.AsNoTracking().FirstOrDefault(a => a.UserID == GetUserId() && a.StorySeriesID == Guid.Parse(StorySeriesID));
            if(creatorSchedule != null)
            {
                return RedirectToAction(nameof(FlowChart), new { id = StorySeriesID });
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 返回故事卡信息
        /// 将故事卡ID设为作家日程表的主卡，并交由Index显示
        /// </summary>
        /// <param name="ID">卡片ID</param>
        /// <param name="ID">卡片ID</param>
        /// <returns></returns>
        public IActionResult GetCard(string StoryCardID,string StorySeriesID)
        {
            CreatorSchedule schedule = _context.CreatorSchedule.FirstOrDefault(a => a.UserID == GetUserId() && a.StorySeriesID == Guid.Parse(StorySeriesID));
            if (schedule != null)
            {
                schedule.StoryCardID = Guid.Parse(StoryCardID);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { id = StorySeriesID });
            }
            else
            {
                return NotFound();
            }
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
        /// <returns>Id 选项ID</returns>
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
                    storyOption.NextStoryCardID = NewStoryCard.ID;
                }
                else
                {
                    nextCardId = storyOption.NextStoryCardID;

                }
                CreatorSchedule schedule = _context.CreatorSchedule.FirstOrDefault(a => a.UserID == GetUserId() && a.StorySeriesID == storyCard.StorySeriesID);
                schedule.StoryCardID = nextCardId;
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), new { id = schedule.StorySeriesID });
            }
            catch
            {
                return NotFound();
            }
        }
    }

}

        