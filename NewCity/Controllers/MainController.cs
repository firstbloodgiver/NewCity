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

        public IActionResult Index()
        {
            var userid = GetUserId();
            if (userid == Guid.Empty) {
                return RedirectToAction("Index", "Home");
            }
            if (isCreator())
            {
                return RedirectToAction("Index", "Creator");
            }


            Guid StorySeriesID = Guid.Empty;
            
            List<StoryCard> OperaList = new List<StoryCard>();


            
            //有无创建人物
            UserCharacter userCharacter = _context.UserCharacter.AsNoTracking().Where(u => u.UserId == userid).FirstOrDefault();
            if (userCharacter == null) {
                CreateCharacter();
            }
            else
            {
                //是否在场景
                if (InLocation(userid, out StorySeriesID))
                {
                    StorySeries ReadList = new StorySeries();
                    ReadList = _context.StorySeries.AsNoTracking().Where(s => s.ID == StorySeriesID).FirstOrDefault();
                    OperaList = _context.StoryCard.AsNoTracking().Where(s => s.StorySeriesID == StorySeriesID).ToList();
                    ViewBag.ReadList = ReadList;
                    ViewBag.OperaList = OperaList;
                }
                else
                {
                    StoryCard ReadList = new StoryCard();
                    var storycardID = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == userid)
                        .Join(_context.CharacterSchedule, a => a.ID, b => b.CharacterID, (a, b) => new { storycardID = b.StoryCardID, b.IsMain })
                        .FirstOrDefault(b => b.IsMain == true);
                    ReadList = _context.StoryCard.AsNoTracking().Include(a => a.StoryOptions).AsNoTracking().SingleOrDefault(a => a.ID == storycardID.storycardID);
                    //去除不符合显示条件的选项
                    List<StoryOption> storyOptions = new List<StoryOption>();
                    foreach (var option in ReadList.StoryOptions)
                    {
                        if (Check(option.Condition, ReadList.StorySeriesID.ToString()))
                        {
                            storyOptions.Add(option);
                        }
                    }
                    ReadList.StoryOptions = storyOptions;
                    ViewBag.ReadList = ReadList;
                }
            }

            

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> NextCard(Guid optionID)
        {
            Guid userid = GetUserId();

            var Schedule = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == userid)
                .Join(_context.CharacterSchedule, a => a.ID,b=>b.CharacterID,(a,b)=>new { IsStory = b.IsStory,IsMain = b.IsMain 
                ,StorySeriesID=b.StorySeriesID, StoryCardID =b.StoryCardID, CharacterID=b.CharacterID })
                .Where(a=>a.IsMain == true).FirstOrDefault();
            if (Schedule.IsStory)
            {
                List<StoryOption> Options = new List<StoryOption>();
                var storycards = _context.StoryCard.AsNoTracking().Where(a => a.ID == Schedule.StoryCardID).Include(a => a.StoryOptions).FirstOrDefault();
                foreach (var option in storycards.StoryOptions)
                {
                    if (Check(option.Condition, storycards.StorySeriesID.ToString()))
                    {
                        Options.Add(option);
                    }
                }
                var opt = Options.Where(a => a.ID == optionID).FirstOrDefault();
                if (opt != null)
                {
                    var card = await _context
                       .StoryCard
                       .AsNoTracking()  
                       .Include(s => s.StoryOptions)
                       .FirstOrDefaultAsync(m => m.ID == opt.NextStoryCardID);
                    JsonResult result = Json(card);
                    #region  
                    SetMain(Schedule.CharacterID);
                    //保存进度
                    Guid CharacterID = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == userid).FirstOrDefault().ID;
                    var characterSchedule = _context.CharacterSchedule.Where(a => a.CharacterID == CharacterID).FirstOrDefault();
                    characterSchedule.StoryCardID = opt.NextStoryCardID;
                    characterSchedule.IsMain = true;
                    _context.CharacterSchedule.Update(characterSchedule);
                    #endregion

                    #region
                    if (!string.IsNullOrWhiteSpace(opt.Effect))
                    {
                        List<Istatus> storyStatuses = JsonConvert.DeserializeObject<List<Istatus>>(opt.Effect);
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
                                case (int)enumEffectType.场景转移:
                                    //记录
                                    SetMain(Schedule.CharacterID);
                                    CharacterSchedule schedule = _context.CharacterSchedule.Where(a => a.StorySeriesID == Guid.Parse(obj.Value) && a.CharacterID == Schedule.CharacterID).FirstOrDefault();
                                    if (schedule != null)
                                    {
                                        schedule.IsMain = true;
                                        schedule.StorySeriesID = Guid.Parse(obj.Value);
                                        _context.CharacterSchedule.Update(schedule);
                                    }
                                    else
                                    {
                                        schedule = new CharacterSchedule()
                                        {
                                            ID = new Guid(),
                                            CharacterID = CharacterID,
                                            StorySeriesID = Guid.Parse(obj.Value),
                                            IsMain = true,
                                            IsStory = false
                                        };

                                        _context.CharacterSchedule.Add(schedule);
                                    }


                                    var cards = _context.StorySeries.AsNoTracking().Where(a => a.ID == Guid.Parse(obj.Value)).FirstOrDefault();
                                    var resultcard =  new 
                                    {
                                        StorySeriesID = cards.ID,
                                        StoryName = cards.SeriesName,
                                        Text = cards.Text,
                                        IMG = cards.IMG,
                                        StoryOptions = _context.StoryCard.Where(a => a.StorySeriesID == cards.ID).ToList()
                                    };
                                    result = Json(resultcard);
                                       
                                    break;
                            }
                        }
                    }
                    _context.SaveChanges();
                    #endregion
                    return result;
                }
            }
            else
            {
                //地图场景处理
                var storycards = _context.StorySeries.AsNoTracking().Where(a => a.ID == Schedule.StorySeriesID).FirstOrDefault();
                var card = new
                {
                    StorySeriesID = storycards.ID,
                    StoryName = storycards.SeriesName,
                    Text = storycards.Text,
                    IMG = storycards.IMG,
                    StoryOptions = _context.StoryCard.Where(a => a.StorySeriesID == storycards.ID).ToList()
                };
                return Json(card);
            }
            return Json("不存在的后续故事卡片！");
        }

        /// <summary>
        /// 获取人物属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetState() {
            var state = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == GetUserId() && a.IsActivate == true).FirstOrDefault();

            if (state != null) {
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
                _context.UserCharacter.AsNoTracking().Where(u=>u.UserId == GetUserId() && u.IsActivate == true).FirstOrDefault().ID)
                .Join(_context.Item,a=>a.ItemID,i=>i.ID,(a,i)=>new { Amount = a.Amount, Introduction = i.Introduction,Name = i.Name})
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
        public string GetSchedule() {
            var schedule = _context.CharacterSchedule.AsNoTracking()
               .Where(a => a.CharacterID ==
               _context.UserCharacter.AsNoTracking()
               .Where(u => u.UserId == GetUserId() && u.IsActivate == true).FirstOrDefault().ID && a.IsStory == true)
               .Join(_context.StorySeries,a=>a.StorySeriesID,s=>s.ID,(a,s)=>new {
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
        private bool Check(string Condition,string StorySeriesID)
        {
            if (!string.IsNullOrWhiteSpace(Condition)) {
                List<StoryStatus> storyStatuses = JsonConvert.DeserializeObject<List<StoryStatus>>(Condition);
                foreach (var status in storyStatuses)
                {
                    var DBstatus = _context.StoryStatus.AsNoTracking().Where(a => a.StorySeries == StorySeriesID && a.Name == status.Name).FirstOrDefault();
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
                .Join(_context.CharacterSchedule.Where(a=>a.IsMain == true),u => u.ID,c => c.CharacterID,(u,c)=>new { c.IsStory,c.StorySeriesID }).FirstOrDefault();

            StorySeriesID = Guid.Empty;
            if (location != null) {
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
        private void CreateCharacter() {
            UserCharacter character = new UserCharacter() {
                ID = Guid.NewGuid(),
                UserId = GetUserId(),
                IsActivate = true,
            };
            _context.UserCharacter.Add(character);

            CharacterSchedule schedule = new CharacterSchedule() {
                ID = Guid.NewGuid(),
                CharacterID = character.ID,
                StoryCardID = new DefaultValue().createCharacter,
                StorySeriesID = new DefaultValue().ScreateCharacter,
                IsMain = true,
                IsStory = true,
            };
            _context.CharacterSchedule.Add(schedule);

            _context.SaveChanges();

            ViewBag.ReadList = _context.StoryCard.AsNoTracking().Include(a => a.StoryOptions).AsNoTracking().SingleOrDefault(a => a.ID == new DefaultValue().createCharacter);
            ViewBag.InLocation = false;

        }



        
        /// <summary>
        /// 返回默认地点故事卡
        /// </summary>
        private IActionResult DefaultLocation() {
            ViewBag.ReadList = _context.StorySeries.AsNoTracking().Where(s => s.ID == new DefaultValue().defaultlocation).FirstOrDefault();
            ViewBag.OperaList = _context.StoryCard.AsNoTracking().Where(s => s.StorySeriesID == new DefaultValue().defaultlocation).ToList();
            ViewBag.InLocation = true;
            return View();
        }

        /// <summary>
        /// 将所有在主页面中的状态设为false
        /// 用于初始化
        /// </summary>
        /// <param name="CharacterID"></param>
        private void SetMain(Guid CharacterID) {
            List<CharacterSchedule> tempSchedule = _context.CharacterSchedule.Where(a => a.CharacterID == CharacterID).ToList();
            foreach (var obj in tempSchedule)
            {
                obj.IsMain = false;
                _context.CharacterSchedule.Update(obj);
            }
        }

        #region 卡片执行操作
        private void Increase(Istatus storyStatus)
        {
            StoryStatus status = _context.StoryStatus.Where(a => a.StorySeries == storyStatus.StorySeries && a.Name == storyStatus.Name).FirstOrDefault();
            if (status != null) {
                status.Value = (Convert.ToInt32(status.Value)+Convert.ToInt32(storyStatus.Value)).ToString();
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

       
        #endregion
    }



}