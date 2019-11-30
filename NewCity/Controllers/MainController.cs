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
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Main(string SeriesID)
         {
            Guid userid = GetUserId();
            if (userid == Guid.Empty)
            {
                return RedirectToAction("PleaseLogin", "Home");
            }
            var StorySeries = _context.StorySeries.AsNoTracking().Where(a => a.ID == Guid.Parse(SeriesID) && a.Author == userid
            && a.Status == enumStoryStatus.测试).FirstOrDefault();
            if (StorySeries != null)
            {
                CleanStory(SeriesID, userid);
                //重新加入

                AddList(SeriesID);
            }
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

        /// <summary>
        /// 清除该用户之前的故事记录
        /// </summary>
        /// <param name="SeriesID"></param>
        /// <param name="userid"></param>
        private void CleanStory(string SeriesID, Guid userid)
        {
            //清除上次运行数据
            UserSchedule UserSchedule = _context.UserSchedule.Where(a => a.UserID == userid && a.StorySeriesID == Guid.Parse(SeriesID)).FirstOrDefault();
            UserCharacter userCharacter = null;
            List<CharacterItem> characterItem = new List<CharacterItem>();
            if (UserSchedule != null)
            {
                userCharacter = _context.UserCharacter.Where(a => a.UserId == userid && a.ID == UserSchedule.CharacterID).FirstOrDefault();
            }
            if (userCharacter != null)
            {
                characterItem = _context.CharacterItem.Where(a => a.CharacterID == userCharacter.ID).ToList();
            }
            if (UserSchedule != null)
            {
                _context.Remove(UserSchedule);
            }
            if (userCharacter != null)
            {
                _context.Remove(userCharacter);
            }
            if (characterItem.Count > 0)
            {
                _context.RemoveRange(characterItem);
            }
            //清除旧状态
            var statuslist = _context.CharacterStatus.Where(a => a.StorySeries == SeriesID && a.UserID == userid).ToList();
            if (statuslist.Count > 0)
            {
                _context.CharacterStatus.RemoveRange(statuslist);
            }
            _context.SaveChanges();
        }

        public IActionResult StorySelect()
        {
            Guid userid = GetUserId();
            if (userid == Guid.Empty)
            {
                return RedirectToAction("PleaseLogin", "Home");
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
                CleanStory(StorySeriesID.ToString(), GetUserId());
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
            if (userid == Guid.Empty)
            {
                return RedirectToAction("PleaseLogin", "Home");
            }
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
                && _context.UserSchedule.AsNoTracking().Where(a => a.StorySeriesID == serID
                && a.UserID == GetUserId()).FirstOrDefault() == null)
            {
                var newCharacter = CreateCharacter(GetUserId(), Guid.Parse(SeriesID));

                var card = _context.StoryCard.AsNoTracking().Where(a => a.IsHead == true && a.StorySeriesID == serID).FirstOrDefault();
                UserSchedule userSchedule = new UserSchedule()
                {
                    ID = Guid.NewGuid(),
                    CharacterID = newCharacter.ID,
                    StorySeriesID = serID,
                    StoryCardID = card == null ? Guid.Empty : card.ID,
                    UserID = GetUserId(),
                    ScheduleStatus = enumStoryStatus.进行中
                };
                _context.UserCharacter.Add(newCharacter);
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
                //校验操作 - 查看该选项是否在故事卡片内并可以选择
                var opti = _context.StoryOption.AsNoTracking().Where(a => a.ID == optionID).First();
                var storycard = _context.StoryCard.Include(a => a.StoryOptions).AsNoTracking().Where(a => a.ID == opti.StoryCardID).First();
                var Schedule = _context.UserSchedule.Where(a => a.UserID == GetUserId() && a.StoryCardID == storycard.ID).First();
                if (Check(opti.Condition, storycard.StorySeriesID.ToString()))
                {
                    var Nextstorycard = _context.StoryCard.Include(a => a.StoryOptions).AsNoTracking().Where(a => a.ID == opti.NextStoryCardID).FirstOrDefault();
                    JsonResult result = Json(string.Empty);
                    if (Nextstorycard != null)
                    {
                        StoryCard resultCard = new StoryCard() {
                            ID = Nextstorycard.ID,
                            StorySeriesID = Nextstorycard.StorySeriesID,
                            StoryName = Nextstorycard.StoryName,
                            Title = Nextstorycard.Title,
                            Text = Nextstorycard.Text,
                            IMG = Nextstorycard.IMG,
                            BackgroundIMG = Nextstorycard.BackgroundIMG,
                            IsHead = Nextstorycard.IsHead,
                            StoryOptions = new List<StoryOption>()
                        };
                        //去除不显示的选项
                        foreach (var option in Nextstorycard.StoryOptions)
                        {
                            if (Check(option.Condition, storycard.StorySeriesID.ToString()))
                            {
                                resultCard.StoryOptions.Add(option);
                            }
                        }
                        result = Json(resultCard);
                        //保存进度
                        #region  
                        Schedule.StoryCardID = Nextstorycard.ID;
                        await _context.SaveChangesAsync();
                        #endregion
                    }
                    //执行操作效果
                    #region
                    if (!string.IsNullOrWhiteSpace(opti.Effect))
                    {
                        List<Istatus> storyStatuses = JsonConvert.DeserializeObject<List<Istatus>>(opti.Effect);
                        foreach (var obj in storyStatuses)
                        {
                            //操作
                            if (Convert.ToInt32(obj.Type) == (int)enumEffectType.结束处理)
                            {
                                return result = Json(GameOver(Schedule.StorySeriesID));
                            }
                            //人物属性
                            UserSchedule schedule = _context.UserSchedule.AsNoTracking().Where(a => a.UserID == GetUserId() && a.StorySeriesID == Schedule.StorySeriesID).First();
                            UserCharacter character = _context.UserCharacter.Where(a => a.ID == schedule.CharacterID).FirstOrDefault();
                            switch (obj.Name)
                            {
                                case ("ActionPoints"):
                                    character.ActionPoints = ExeCharacterEffect(character.ActionPoints, float.Parse(obj.Value), obj.Type);
                                    break;
                                case ("Lucky"):
                                    character.Lucky = ExeCharacterEffect(character.Lucky, float.Parse(obj.Value), obj.Type);
                                    break;
                                case ("Speed"):
                                    character.Speed = ExeCharacterEffect(character.Speed, float.Parse(obj.Value), obj.Type);
                                    break;
                                case ("Strength"):
                                    character.Strength = ExeCharacterEffect(character.Strength, float.Parse(obj.Value), obj.Type);
                                    break;
                                case ("Intelligence"):
                                    character.Intelligence = ExeCharacterEffect(character.Intelligence, float.Parse(obj.Value), obj.Type);
                                    break;
                                case ("Experience"):
                                    character.Experience = ExeCharacterEffect(character.Experience, float.Parse(obj.Value), obj.Type);
                                    break;
                                case ("Status"):
                                    character.Status = ExeCharacterEffect(character.Status, float.Parse(obj.Value), obj.Type);
                                    break;
                                case ("Moral"):
                                    character.Moral = ExeCharacterEffect(character.Moral, float.Parse(obj.Value), obj.Type);
                                    break;
                                default:
                                    //道具
                                    //TODO

                                    //故事状态
                                    storystatu(userid, storycard, obj);
                                    break;
                            }
                            _context.SaveChanges();
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
            catch(Exception ex)
            {
                return Json(ex);
            }
        }

        /// <summary>
        /// 故事状态
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="storycard"></param>
        /// <param name="obj"></param>
        private void storystatu(Guid userid, StoryCard storycard, Istatus obj)
        {
            CharacterStatus status = _context.CharacterStatus.Where(a => a.StorySeries == storycard.StorySeriesID.ToString()
                                                && a.Name == obj.Name
                                                && a.UserID == userid).FirstOrDefault();
            if (status != null)
            {
                status.Value = ExeCharacterEffect(float.Parse(status.Value), float.Parse(obj.Value), obj.Type).ToString();
                _context.CharacterStatus.Update(status);
            }
            else
            {
                CharacterStatus NewcharacterStatus = new CharacterStatus()
                {
                    UserID = userid,
                    ID = Guid.NewGuid(),
                    StorySeries = storycard.StorySeriesID.ToString(),
                    Name = obj.Name,
                    Value = ExeCharacterEffect(0, float.Parse(obj.Value), obj.Type).ToString()
                };
                _context.CharacterStatus.Add(NewcharacterStatus);
            }
        }



        /// <summary>
        /// 效果执行
        /// </summary>
        /// <param name="status"></param>
        /// <param name="num"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private float ExeCharacterEffect(float status, float num, string type)
        {
            switch (Convert.ToInt32(type))
            {
                case (int)enumEffectType.增加:
                    status += num;
                    break;
                case (int)enumEffectType.减少:
                    status -= num;
                    break;
                case (int)enumEffectType.赋值:
                    status = num;
                    break;
            }
            return status;
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
        public string GetItem(Guid StorySeriesID)
        {
            try
            {
                List<Guid> SeriesList = new List<Guid>();
                _context.UserSchedule.AsNoTracking().Where(a => a.UserID == GetUserId() && a.StorySeriesID == StorySeriesID && a.IsMain == true).First();
                var itesm = _context.Item.AsNoTracking().Where(a => a.StorySeriesID == StorySeriesID).First();
                return JsonConvert.SerializeObject(itesm);
            }
            catch
            {
                return "[]";
            }
        }

        [HttpPost]
        public JsonResult useItem(Guid itemID)
        {
            try
            {
                //发动效果，修改玩家状态，减少道具数量
                ExeEffect(itemID);
                return Json("true");
            }
            catch
            {
                return Json("");
            }
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

        private UserCharacter CreateCharacter(Guid UserID, Guid StorySeriesID)
        {
            UserCharacter character = new UserCharacter()
            {
                ID = Guid.NewGuid(),
                UserId = UserID,
                CharacterName = string.Empty,
                IsActivate = true,
                ActionPoints = 5,
                Lucky = 5,
                Speed = 5,
                Strength = 5,
                Intelligence = 5,
                Experience = 5,
                Status = 5,
                Moral = 5
            };

            return character;
        }

        /// <summary>
        /// 执行道具效果
        /// </summary>
        private void ExeEffect(Guid itemID)
        {
            Item item = _context.Item.AsNoTracking().Where(a => a.ID == itemID).First();
            UserSchedule schedule = _context.UserSchedule.Where(a => a.UserID == GetUserId() && a.IsMain == true).First();
            UserCharacter character = _context.UserCharacter.Where(a => a.ID == schedule.CharacterID).First();
            var characterItem = _context.CharacterItem.Where(a => a.CharacterID == character.ID && a.ID == itemID).First();

            exeeffect(item.EffectClass1, item.EffectName1, item.EffectType1, item.EffectValue1, character, schedule);
            exeeffect(item.EffectClass2, item.EffectName2, item.EffectType2, item.EffectValue2, character, schedule);
            exeeffect(item.EffectClass3, item.EffectName3, item.EffectType3, item.EffectValue3, character, schedule);
            exeeffect(item.EffectClass4, item.EffectName4, item.EffectType4, item.EffectValue4, character, schedule);
            exeeffect(item.EffectClass5, item.EffectName5, item.EffectType5, item.EffectValue5, character, schedule);

            characterItem.Amount--;
            _context.SaveChanges();
        }

        private void exeeffect(string EffectClass, string EffectName, string EffectType, string EffectValue, UserCharacter character, UserSchedule schedule)
        {
            switch (EffectClass)
            {
                case "1"://故事状态
                    var status = _context.StoryStatus.Where(a => a.Name == EffectName && a.StorySeries == schedule.StorySeriesID.ToString()).First();
                    status.Value = execharacter(EffectValue, EffectType, float.Parse(status.Value)).ToString();
                    break;
                case "2"://角色状态
                    switch (EffectName)
                    {
                        case "ActionPoints":
                            character.ActionPoints = execharacter(EffectValue, EffectType, character.ActionPoints);
                            break;
                        case "Lucky":
                            character.Lucky = execharacter(EffectValue, EffectType, character.Lucky);
                            break;
                        case "Speed":
                            character.Speed = execharacter(EffectValue, EffectType, character.Speed);
                            break;
                        case "Strength":
                            character.Strength = execharacter(EffectValue, EffectType, character.Strength);
                            break;
                        case "Intelligence":
                            character.Intelligence = execharacter(EffectValue, EffectType, character.Intelligence);
                            break;
                        case "Experience":
                            character.Experience = execharacter(EffectValue, EffectType, character.Experience);
                            break;
                        case "Status":
                            character.Status = execharacter(EffectValue, EffectType, character.Status);
                            break;
                        case "Moral":
                            character.Moral = execharacter(EffectValue, EffectType, character.Moral);
                            break;
                    }
                    break;
            }
        }

        private float execharacter(string value, string type, float status)
        {
            float result = 0;
            switch (type)
            {
                case "0": //增加
                    result = status + float.Parse(value);
                    break;
                case "1"://减少
                    result = status - float.Parse(value);
                    break;
                case "2"://赋值
                    result = float.Parse(value);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 检查是否符合显示条件
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        private bool Check(string Condition, string StorySeriesID)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Condition) && Condition != "[]")
                {
                    List<StoryStatus> storyStatuses = JsonConvert.DeserializeObject<List<StoryStatus>>(Condition);
                    foreach (var status in storyStatuses)
                    {
                        CharacterStatus DBstatus = _context.CharacterStatus.AsNoTracking().Where(a => a.StorySeries == StorySeriesID 
                        && a.Name == status.Name && a.UserID == GetUserId()).FirstOrDefault();
                        if (DBstatus == null)
                        {
                            var schedule = _context.UserSchedule.AsNoTracking().Where(a => a.UserID == GetUserId() && a.StorySeriesID == Guid.Parse(StorySeriesID)).First();
                            string charactervalue = string.Empty;
                            switch (status.Name)
                            {
                                case ("ActionPoints"):
                                    charactervalue = _context.UserCharacter.AsNoTracking().Where(a => a.ID == schedule.CharacterID).First().ActionPoints.ToString();
                                    break;
                                case ("Lucky"):
                                    charactervalue = _context.UserCharacter.AsNoTracking().Where(a => a.ID == schedule.CharacterID).First().Lucky.ToString();
                                    break;
                                case ("Speed"):
                                    charactervalue = _context.UserCharacter.AsNoTracking().Where(a => a.ID == schedule.CharacterID).First().Speed.ToString();
                                    break;
                                case ("Strength"):
                                    charactervalue = _context.UserCharacter.AsNoTracking().Where(a => a.ID == schedule.CharacterID).First().Strength.ToString();
                                    break;
                                case ("Intelligence"):
                                    charactervalue = _context.UserCharacter.AsNoTracking().Where(a => a.ID == schedule.CharacterID).First().Intelligence.ToString();
                                    break;
                                case ("Experience"):
                                    charactervalue = _context.UserCharacter.AsNoTracking().Where(a => a.ID == schedule.CharacterID).First().Experience.ToString();
                                    break;
                                case ("Status"):
                                    charactervalue = _context.UserCharacter.AsNoTracking().Where(a => a.ID == schedule.CharacterID).First().Status.ToString();
                                    break;
                                case ("Moral"):
                                    charactervalue = _context.UserCharacter.AsNoTracking().Where(a => a.ID == schedule.CharacterID).First().Moral.ToString();
                                    break;
                                default:
                                    return false;

                            }
                            CharacterStatus characterstatus = new CharacterStatus()
                            {
                                Value = charactervalue
                            };
                            DBstatus = characterstatus;
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
            catch
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
        //private IActionResult DefaultLocation()
        //{
        //    ViewBag.ReadList = _context.StorySeries.AsNoTracking().Where(s => s.ID == new DefaultValue().defaultlocation).FirstOrDefault();
        //    ViewBag.OperaList = _context.StoryCard.AsNoTracking().Where(s => s.StorySeriesID == new DefaultValue().defaultlocation).ToList();
        //    ViewBag.InLocation = true;
        //    return View();
        //}



        /// <summary>
        /// 结束操作
        /// </summary>
        /// <param name="StorySeriesID"></param>
        private string GameOver(Guid StorySeriesID)
        {
            var Schedule = _context.UserSchedule.Where(a => a.StorySeriesID == StorySeriesID && a.UserID == GetUserId()).FirstOrDefault();
            enumStoryStatus status = _context.StorySeries.AsNoTracking().Where(a => a.ID == StorySeriesID).First().Status;
            if (Schedule != null)
            {
                if (status == enumStoryStatus.测试)
                {
                    Schedule.ScheduleStatus = enumStoryStatus.结束;
                    _context.SaveChanges();
                    return "TestOver";
                }
                else if (Schedule.ScheduleStatus== enumStoryStatus.进行中)
                {
                    Schedule.ScheduleStatus = enumStoryStatus.结束;
                    _context.SaveChanges();
                    return "GameOver";
                }
            }
            return "GameOver";
        }
    }
}