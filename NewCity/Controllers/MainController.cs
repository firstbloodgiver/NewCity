using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NewCity.Data;
using NewCity.Models;
using System.Web;
using Newtonsoft.Json;
using NewCity.Enum;


namespace NewCity.Controllers
{
    public class MainController : BaseController
    {
        public MainController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context)
            : base(SignInManager, UserManager, context)
        {
        }

        /*准备可弃用*/
        //public IActionResult Index()
        //{
        //    var userid = GetUserId();
        //    if (userid == Guid.Empty) {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    if (isCreator())
        //    {
        //        return RedirectToAction("Index", "Creator");
        //    }


        //    Guid StorySeriesID = Guid.Empty;

        //    List<StoryCard> OperaList = new List<StoryCard>();



        //    //有无创建人物
        //    UserCharacter userCharacter = _context.UserCharacter.AsNoTracking().Where(u => u.UserId == userid).FirstOrDefault();
        //    if (userCharacter == null) {
        //        CreateCharacter();
        //    }
        //    else 
        //    {
        //        //是否在场景
        //        if (InLocation(userid, out StorySeriesID))
        //        {
        //            StorySeries ReadList = new StorySeries();
        //            ReadList = _context.StorySeries.AsNoTracking().Where(s => s.ID == StorySeriesID).FirstOrDefault();
        //            OperaList = _context.StoryCard.AsNoTracking().Where(s => s.StorySeriesID == StorySeriesID).ToList();
        //            ViewBag.ReadList = ReadList;
        //            ViewBag.OperaList = OperaList;
        //        }
        //        else
        //         {
        //            StoryCard ReadList = new StoryCard();
        //            var storycardID = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == userid)
        //                .Join(_context.UserSchedule, a => a.ID, b => b.CharacterID, (a, b) => new { storycardID = b.StoryCardID, b.IsMain })
        //                .FirstOrDefault(b => b.IsMain == true);
        //            ReadList = _context.StoryCard.AsNoTracking().Include(a => a.StoryOptions).AsNoTracking().SingleOrDefault(a => a.ID == storycardID.storycardID);
        //            //去除不符合显示条件的选项
        //            List<StoryOption> storyOptions = new List<StoryOption>();
        //            foreach (var option in ReadList.StoryOptions)
        //            {
        //                if (Check(option.Condition, ReadList.StorySeriesID.ToString()))
        //                {
        //                    storyOptions.Add(option);
        //                }
        //            }
        //            ReadList.StoryOptions = storyOptions;
        //            ViewBag.ReadList = ReadList;
        //        }
        //    }
        //    return View();
        //}

        public IActionResult Index()
        {
            Guid userid = GetUserId();
            if (userid == Guid.Empty)
            {
                return RedirectToAction("Index", "Home");
            }
            if (isCreator())
            {
                return RedirectToAction("Index", "Creator");
            }
            else
            {
                return RedirectToAction(nameof(StorySelect));
            }
        }

        public IActionResult Main(string SeriesID)
        {
            Guid userid = GetUserId();
            var Schedule = _context.UserSchedule.Where(a => a.UserID == userid && a.StorySeriesID == Guid.Parse(SeriesID)).FirstOrDefault();
            var Card = _context.StoryCard.Include(a => a.StoryOptions).AsNoTracking().Where(a => a.ID == Schedule.StoryCardID).FirstOrDefault();
            //去除不显示得选项
            List<StoryOption> storyOptions = new List<StoryOption>();
            foreach (var option in Card.StoryOptions)
            {
                if (Check(option.Condition, Card.StorySeriesID.ToString()))
                {
                    storyOptions.Add(option);
                }
            }
            ViewBag.StoryOptions = storyOptions;
            ViewBag.StoryCard = Card;
            return View();
        }

        public IActionResult StorySelect()
        {
            Guid userid = GetUserId();
            if (userid == Guid.Empty)
            {
                return RedirectToAction("Index", "Home");
            }

            List<UserSchedule> userSchedules = _context.UserSchedule.AsNoTracking().Where(a => a.UserID == userid).ToList();
            List<StorySeries> storySeriesList = new List<StorySeries>();
            List<StoryCard> storyCards = new List<StoryCard>();
            foreach (var schedules in userSchedules)
            {
                var Series = _context.StorySeries.AsNoTracking().Where(a => a.ID == schedules.StorySeriesID && a.Status == enumStoryStatus.进行中).FirstOrDefault();
                var card = _context.StoryCard.AsNoTracking().Where(a => a.ID == schedules.StoryCardID).FirstOrDefault();
                if (Series != null)
                {
                    Series.Status = schedules.ScheduleStatus;
                    storySeriesList.Add(Series);
                }
                if (card != null)
                {
                    storyCards.Add(card);
                }
            }
            ViewBag.storySeriesList = storySeriesList;
            ViewBag.storyCards = storyCards;
            return View();
        }

        [HttpPost]
        public JsonResult Restart(Guid StorySeriesID)
        {
            try
            {
                var Schedule = _context.UserSchedule.Where(a => a.StorySeriesID == StorySeriesID && a.UserID == GetUserId()).First();
                Schedule.StoryCardID = _context.StoryCard.AsNoTracking().Where(a => a.StorySeriesID == StorySeriesID && a.IsHead == true).First().ID;
                Schedule.ScheduleStatus = enumStoryStatus.进行中;
                _context.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        public async Task<IActionResult> NewStory(string sortOrder, string searchString, string currentFilter, int? pagenumber)
        {
            Guid userid = GetUserId();
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pagenumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var storySeries = from s in _context.StorySeries where s.Status == enumStoryStatus.进行中 select s;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                storySeries = storySeries.Where(a => a.SeriesName.Contains(searchString) && a.Status == enumStoryStatus.测试);
            }

            switch (sortOrder)
            {
                case "Date":
                    storySeries = storySeries.OrderBy(a => a.Creationdate);
                    break;
                default:
                    storySeries = storySeries.OrderBy(a => a.Creationdate);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<StorySeries>.CreateAsync(storySeries, pagenumber ?? 1, pageSize));
        }

        public IActionResult SeriesDetail(string SeriesID)
        {
            var userid = GetUserId();
            bool HadAdd = false;

            StorySeries storySeries = _context.StorySeries.AsNoTracking().Where(a => a.ID == Guid.Parse(SeriesID)).FirstOrDefault();
            if (_context.UserSchedule.AsNoTracking().Where(a => a.UserID == userid && a.StorySeriesID == Guid.Parse(SeriesID)).FirstOrDefault() != null)
            {
                HadAdd = true;
            }
            ViewBag.HadAdd = HadAdd;
            if (storySeries.Status == enumStoryStatus.测试)
            {
                return NotFound();
            }
            return View(storySeries);
        }

        /// <summary>
        /// 添加到列表
        /// </summary>
        /// <param name="SeriesID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddList(string SeriesID)
        {
            Guid serID = Guid.Parse(SeriesID);
            if (_context.StorySeries.AsNoTracking().Where(a => a.ID == serID).FirstOrDefault() != null
                && _context.UserSchedule.AsNoTracking().Where(a => a.StorySeriesID == serID).FirstOrDefault() == null)
            {
                var card = _context.StoryCard.AsNoTracking().Where(a => a.IsHead == true && a.StorySeriesID == serID).FirstOrDefault();
                UserSchedule userSchedule = new UserSchedule()
                {
                    ID = Guid.NewGuid(),
                    CharacterID = Guid.Empty,
                    StorySeriesID = serID,
                    StoryCardID = card == null ? Guid.Empty : card.ID,
                    UserID = GetUserId()
                };
                _context.UserSchedule.Add(userSchedule);
                _context.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        [HttpPost]
        public async Task<JsonResult> NextCard(Guid optionID)
        {
            Guid userid = GetUserId();
            try
            {
                //查看该选项是否在故事卡片内并可以选择
                var opti = _context.StoryOption.AsNoTracking().Where(a => a.ID == optionID).First();
                var storycard = _context.StoryCard.Include(a => a.StoryOptions).AsNoTracking().Where(a => a.ID == opti.StoryCardID).First();
                var Schedule = _context.UserSchedule.Where(a => a.UserID == GetUserId() && a.StoryCardID == storycard.ID).First();
                if (Check(opti.Condition, storycard.StorySeriesID.ToString()))
                {
                    var Nextstorycard = _context.StoryCard.Include(a => a.StoryOptions).AsNoTracking().Where(a => a.ID == opti.NextStoryCardID).FirstOrDefault();
                    JsonResult result = Json(storycard);
                    if (Nextstorycard != null)
                    {
                        result = Json(Nextstorycard);
                        #region  
                        //保存进度
                        Schedule.StoryCardID = Nextstorycard.ID;
                        await _context.SaveChangesAsync();
                        #endregion
                    }
                    #region
                    //执行操作效果
                    if (!string.IsNullOrWhiteSpace(opti.Effect))
                    {
                        List<Istatus> storyStatuses = JsonConvert.DeserializeObject<List<Istatus>>(opti.Effect);
                        foreach (var obj in storyStatuses)
                        {
                            switch (Convert.ToInt32(obj.Type))
                            {
                                case (int)enumEffectType.增加:
                                    Increase(obj);
                                    break;
                                case (int)enumEffectType.减少:
                                    Reduce(obj);
                                    break;
                                case (int)enumEffectType.赋值:
                                    Assign(obj);
                                    break;
                                case (int)enumEffectType.结束处理:
                                    GameOver(Schedule.StorySeriesID);
                                    return result = Json("GameOver");
                                    //case (int)enumEffectType.场景转移:
                                    //记录
                                    //UserSchedule schedule = _context.UserSchedule.Where(a => a.StorySeriesID == Guid.Parse(obj.Value) && a.CharacterID == Schedule.CharacterID).FirstOrDefault();
                                    //if (schedule != null)
                                    //{
                                    //    schedule.IsMain = true;
                                    //    schedule.StorySeriesID = Guid.Parse(obj.Value);
                                    //    _context.UserSchedule.Update(schedule);
                                    //}
                                    //else
                                    //{
                                    //    schedule = new UserSchedule()
                                    //    {
                                    //        ID = new Guid(),
                                    //        CharacterID = CharacterID,
                                    //        StorySeriesID = Guid.Parse(obj.Value),
                                    //        IsMain = true,
                                    //        IsStory = false
                                    //    };

                                    //    _context.UserSchedule.Add(schedule);
                                    //}
                                    //var cards = _context.StorySeries.AsNoTracking().Where(a => a.ID == Guid.Parse(obj.Value)).FirstOrDefault();
                                    //var resultcard = new
                                    //{
                                    //    StorySeriesID = cards.ID,
                                    //    StoryName = cards.SeriesName,
                                    //    Text = cards.Text,
                                    //    IMG = cards.IMG,
                                    //    StoryOptions = _context.StoryCard.Where(a => a.StorySeriesID == cards.ID).ToList()
                                    //};
                                    //result = Json(resultcard); 
                                    //break;
                            }
                        }
                    }
                    #endregion
                    return result;
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

        /// <summary>
        /// 获取人物属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetState()
        {
            var state = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == GetUserId() && a.IsActivate == true).FirstOrDefault();

            if (state != null)
            {
                var status = new
                {
                    行动力 = state.ActionPoints,
                    敏捷 = state.Speed,
                    力量 = state.Strength,
                    智力 = state.Intelligence,
                    幸运 = state.Lucky,
                    社会经验 = state.Experience,
                    地位 = state.Status,
                    道德 = state.Moral,
                };
                return JsonConvert.SerializeObject(status);
            }
            return "[]";

        }


        /// <summary>
        /// 获取人物物品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetItem()
        {
            var items = _context.CharacterItem.AsNoTracking()
                .Where(a => a.CharacterID ==
                _context.UserCharacter.AsNoTracking().Where(u => u.UserId == GetUserId() && u.IsActivate == true).FirstOrDefault().ID)
                .Join(_context.Item, a => a.ItemID, i => i.ID, (a, i) => new { Amount = a.Amount, Introduction = i.Introduction, Name = i.Name })
                .ToList();

            if (items != null)
            {
                return JsonConvert.SerializeObject(items);
            }
            return "[]";
        }

        /// <summary>
        /// 获取人物任务表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetSchedule()
        {
            var schedule = _context.UserSchedule.AsNoTracking()
               .Where(a => a.CharacterID ==
               _context.UserCharacter.AsNoTracking()
               .Where(u => u.UserId == GetUserId() && u.IsActivate == true).FirstOrDefault().ID && a.IsStory == true)
               .Join(_context.StorySeries, a => a.StorySeriesID, s => s.ID, (a, s) => new
               {
                   SeriesName = s.SeriesName,
                   Text = s.Text
               })
               .ToList();

            if (schedule != null)
            {
                return JsonConvert.SerializeObject(schedule);
            }
            return "[]";
        }

        /// <summary>
        /// 检查是否符合显示条件
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        private bool Check(string Condition, string StorySeriesID)
        {
            if (!string.IsNullOrWhiteSpace(Condition) && Condition != "[]")
            {
                List<StoryStatus> storyStatuses = JsonConvert.DeserializeObject<List<StoryStatus>>(Condition);
                foreach (var status in storyStatuses)
                {
                    var DBstatus = _context.StoryStatus.AsNoTracking().Where(a => a.StorySeries == StorySeriesID && a.Name == status.Name).FirstOrDefault();
                    if(DBstatus == null)
                    {
                        List<string> characterSyayus = new List<string> { "ActionPoints", "Lucky", "Speed", "Strength", "Intelligence", "Experience", "Status", "Moral" };
                        int pst = characterSyayus.IndexOf(Condition); //位置
                        if(pst != -1)
                        {
                            DBstatus = 
                        }
                    }
                    if (DBstatus != null)
                    {
                        int dbstatus = Convert.ToInt32(DBstatus.Value);
                        int statues = Convert.ToInt32(status.Value);
                        bool result = false;
                        switch (Convert.ToInt32(status.Type))
                        {
                            case (int)enumConditionType.大于:
                                result = dbstatus > statues ? true : false;
                                break;
                            case (int)enumConditionType.小于:
                                result = dbstatus < statues ? true : false;
                                break;
                            case (int)enumConditionType.等于:
                                result = dbstatus == statues ? true : false;
                                break;
                            case (int)enumConditionType.不等于:
                                result = dbstatus != statues ? true : false;
                                break;
                        }
                        if (result == false) return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检测在故事卡中还是在场景中
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="StorySeriesID"></param>
        /// <returns></returns>
        private bool InLocation(Guid userid, out Guid StorySeriesID)
        {
            var location = _context.UserCharacter.AsNoTracking().Where(p => p.UserId == userid && p.IsActivate == true)
                .Join(_context.UserSchedule.Where(a => a.IsMain == true), u => u.ID, c => c.CharacterID, (u, c) => new { c.IsStory, c.StorySeriesID }).FirstOrDefault();

            StorySeriesID = Guid.Empty;
            if (location != null)
            {
                StorySeriesID = location.StorySeriesID;
                if (location.IsStory == true)
                {
                    ViewBag.InLocation = false;
                    return false;
                }
            }

            ViewBag.InLocation = true;
            return true;
        }



        /// <summary>
        /// 返回创建人物故事卡
        /// </summary>
        private void CreateCharacter()
        {
            UserCharacter character = new UserCharacter()
            {
                ID = Guid.NewGuid(),
                UserId = GetUserId(),
                IsActivate = true,
            };
            _context.UserCharacter.Add(character);

            UserSchedule schedule = new UserSchedule()
            {
                ID = Guid.NewGuid(),
                CharacterID = character.ID,
                StoryCardID = new DefaultValue().createCharacter,
                StorySeriesID = new DefaultValue().ScreateCharacter,
                IsMain = true,
                IsStory = true,
            };
            _context.UserSchedule.Add(schedule);

            _context.SaveChanges();

            ViewBag.ReadList = _context.StoryCard.AsNoTracking().Include(a => a.StoryOptions).AsNoTracking().SingleOrDefault(a => a.ID == new DefaultValue().createCharacter);
            ViewBag.InLocation = false;

        }




        /// <summary>
        /// 返回默认地点故事卡
        /// </summary>
        private IActionResult DefaultLocation()
        {
            ViewBag.ReadList = _context.StorySeries.AsNoTracking().Where(s => s.ID == new DefaultValue().defaultlocation).FirstOrDefault();
            ViewBag.OperaList = _context.StoryCard.AsNoTracking().Where(s => s.StorySeriesID == new DefaultValue().defaultlocation).ToList();
            ViewBag.InLocation = true;
            return View();
        }

        

        #region 卡片执行操作
        private void Increase(Istatus storyStatus)
        {
            StoryStatus status = _context.StoryStatus.Where(a => a.StorySeries == storyStatus.StorySeries && a.Name == storyStatus.Name).FirstOrDefault();
            if (status != null)
            {
                status.Value = (Convert.ToInt32(status.Value) + Convert.ToInt32(storyStatus.Value)).ToString();
                _context.StoryStatus.Update(status);

            }
        }
        private void Reduce(Istatus storyStatus)
        {
            StoryStatus status = _context.StoryStatus.Where(a => a.StorySeries == storyStatus.StorySeries && a.Name == storyStatus.Name).FirstOrDefault();
            if (status != null)
            {
                status.Value = (Convert.ToInt32(status.Value) - Convert.ToInt32(storyStatus.Value)).ToString();
                _context.StoryStatus.Update(status);

            }
        }
        private void Assign(Istatus storyStatus)
        {
            StoryStatus status = _context.StoryStatus.Where(a => a.StorySeries == storyStatus.StorySeries && a.Name == storyStatus.Name).FirstOrDefault();
            if (status != null)
            {
                status.Value = storyStatus.Value;
                _context.StoryStatus.Update(status);

            }
        } 
        private void GameOver(Guid StorySeriesID)
        {
            var Schedule = _context.UserSchedule.Where(a => a.StorySeriesID == StorySeriesID && a.UserID == GetUserId()).FirstOrDefault();
            if (Schedule != null)
            {
                Schedule.ScheduleStatus = enumStoryStatus.结束;
                _context.SaveChanges();
            }
        }

        #endregion
    }



}