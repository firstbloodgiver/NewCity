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
            catch
            {
                return NotFound();
            }
            
        }

        /// <summary>
        /// 故事卡树界面
        /// </summary>
        List<StoryCardTree> storyCardTrees = new List<StoryCardTree>();
        List<StoryCard> storyCards = new List<StoryCard>();
        public IActionResult FlowChart(string id)
        {
            #region 故事卡树
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
            #endregion
            #region 故事状态
            ViewBag.StoryStatus = _context.StoryStatus.AsNoTracking().Where(a => a.StorySeries == id).ToList();
            #endregion
            #region 故事道具
            ViewBag.StoryItems = _context.Item.AsNoTracking().Where(a => a.StorySeriesID == Guid.Parse(id)).ToList();
            #endregion
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
                FatherStoryCardId = new List<Guid>(),
                Level = level,
                StoryCard = storyCard
            };
            storyCardTree.FatherStoryCardId.Add(fatherStoryCardId);
            storyCardTrees.Add(storyCardTree);
            foreach (var option in storyCard.StoryOptions)
            {
                StoryCardTree storyCardT = storyCardTrees.FirstOrDefault(a => a.StoryCard.ID == option.NextStoryCardID);
                if (storyCardT==null) {
                    StoryCard ChildStoryCard = storyCards.Where(a => a.ID == option.NextStoryCardID).FirstOrDefault();
                    if (ChildStoryCard != null)
                    {
                        CreateTree(ChildStoryCard, storyCard.ID, level + 1);
                    }
                }
                else
                {
                    storyCardT.FatherStoryCardId.Add(storyCard.ID);
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
                storyCard.Title = card.Title;
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

        /// <summary>
        /// 删除卡片
        /// </summary>
        /// <param name="cardid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteCard(Guid cardid)
        {
            try
            {
                var card = _context.StoryCard.Where(a => a.ID == cardid).First();
                var series = _context.StorySeries.AsNoTracking().Where(a => a.ID == card.StorySeriesID).FirstOrDefault();
                if (series != null && series.Author == GetUserId())
                {
                    //删除选项
                    _context.RemoveRange(_context.StoryOption.Where(a => a.StoryCardID == cardid));
                    _context.Remove(card);
                    _context.SaveChanges();
                }
                return Json("true");
            }
            catch
            {
                return Json("false");
            }
        }

        /// <summary>
        /// 增加状态
        /// </summary>
        /// <param name="storyseries"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Addstatus(string storyseries, string status)
        {
            try
            {
                StoryStatus storyStatus = new StoryStatus()
                {
                    ID = Guid.NewGuid(),
                    StorySeries = storyseries,
                    Name = status

                };
                if( _context.StoryStatus.Where(a=>a.StorySeries == storyseries && a.Name.Equals(status)).FirstOrDefault() == null)
                {
                    _context.StoryStatus.Add(storyStatus);
                    _context.SaveChanges();
                    return Json(true);
                }
                return Json("不允许重复状态名！");
            }
            catch
            {
                return Json("保存时发生错误！");
            }
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult removestatus(Guid statusID)
        {
            try
            {
                var status = _context.StoryStatus.Where(a => a.ID == statusID).First();
                var series = _context.StorySeries.AsNoTracking().Where(a => a.ID == Guid.Parse(status.StorySeries)).FirstOrDefault();
                if(series != null)
                {
                    _context.Remove(status);
                    _context.SaveChanges();
                    return Json("true");
                }
                return Json("未找到该状态！");
            }
            catch
            {
                return Json("未找到该状态！");
            }
        }

        /// <summary>
        /// 编辑状态
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult editstatus(Guid statusID,string status)
        {
            try
            {
                var DBstatus = _context.StoryStatus.Where(a => a.ID == statusID).First();
                var series = _context.StorySeries.AsNoTracking().Where(a => a.ID == Guid.Parse(DBstatus.StorySeries)).FirstOrDefault();
                if (series != null)
                {
                    DBstatus.Name = status;
                    _context.SaveChanges();
                    return Json("true");
                }
                return Json("未找到该状态！");
            }
            catch
            {
                return Json("未找到该状态！");
            }
            
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="StorySeriesID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getStatus(string StorySeriesID)
        {
            var result = new List<StoryStatus>();
            try
            {
                if (_context.StorySeries.AsNoTracking().Where(a => a.ID == Guid.Parse(StorySeriesID)).First().Author == GetUserId())
                {
                    result = _context.StoryStatus.AsNoTracking().Where(a => a.StorySeries == StorySeriesID).ToList();
                }
                return Json(result);
            }
            catch
            {
                return Json(result);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(string itemID,Guid StorySeriesID, [Bind("Name,Introduction" +
            ",EffectType1,EffectType1,EffectValue1" +
            ",EffectType2,EffectType2,EffectValue2" +
            ",EffectType3,EffectType3,EffectValue3" +
            ",EffectType4,EffectType4,EffectValue4" +
            ",EffectType5,EffectType5,EffectValue5")] Item item)
        {
            try
            {
                var story = _context.StorySeries.Where(a => a.ID == StorySeriesID).First();
                if(story.Author == GetUserId())
                {
                    if (itemID != string.Empty)
                    {
                        var olditem = _context.Item.Where(a => a.ID == Guid.Parse(itemID)).First();
                        olditem.Name = item.Name;
                        olditem.Introduction = item.Introduction;
                        olditem.EffectName1 = item.EffectName1;
                        olditem.EffectType1 = item.EffectType1;
                        olditem.EffectValue1 = item.EffectValue1;
                        olditem.EffectName2 = item.EffectName2;
                        olditem.EffectType2 = item.EffectType2;
                        olditem.EffectValue2 = item.EffectValue2;
                        olditem.EffectName3 = item.EffectName3;
                        olditem.EffectType3 = item.EffectType3;
                        olditem.EffectValue3 = item.EffectValue3;
                        olditem.EffectName4 = item.EffectName4;
                        olditem.EffectType4 = item.EffectType4;
                        olditem.EffectValue4 = item.EffectValue4;
                        olditem.EffectName5 = item.EffectName5;
                        olditem.EffectType5 = item.EffectType5;
                        olditem.EffectValue5 = item.EffectValue5;

                        await _context.SaveChangesAsync();
                        return Json("true");
                    }
                    else
                    {
                        Item newitem = new Item()
                        {
                            ID = Guid.NewGuid(),
                            StorySeriesID = StorySeriesID,
                            Name = item.Name,
                            Introduction = item.Introduction,
                            EffectName1 = item.EffectName1,
                            EffectType1 = item.EffectType1,
                            EffectValue1 = item.EffectValue1,
                            EffectName2 = item.EffectName2,
                            EffectType2 = item.EffectType2,
                            EffectValue2 = item.EffectValue2,
                            EffectName3 = item.EffectName3,
                            EffectType3 = item.EffectType3,
                            EffectValue3 = item.EffectValue3,
                            EffectName4 = item.EffectName4,
                            EffectType4 = item.EffectType4,
                            EffectValue4 = item.EffectValue4,
                            EffectName5 = item.EffectName5,
                            EffectType5 = item.EffectType5,
                            EffectValue5 = item.EffectValue5,
                        };
                        await _context.Item.AddAsync(newitem);
                        return Json("true");
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public JsonResult getitem(Guid itemID)
        {
            try
            {
                var item = _context.Item.AsNoTracking().Where(a => a.ID == itemID).First();
                _context.StorySeries.AsNoTracking().Where(a => a.ID == item.ID && a.Author == GetUserId()).First();
                return Json(item);
            }
            catch
            {
                return Json(new Item());
            }
            
        }

        /// <summary>
        /// 删除道具
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult removeitem(Guid itemID)
        {
            try
            {
                var item =_context.Item.Where(a => a.ID == itemID).First();
                if (_context.StorySeries.AsNoTracking().Where(a => a.ID == item.StorySeriesID).First().Author == GetUserId())
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch
            {
                return Json(false);
            }
        }
    }

}

        