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

            Guid StorySeriesID = Guid.Empty;
            
            List<StoryCard> OperaList = new List<StoryCard>();

            if (isCreator())
            {
                ViewData["Creator"] = true;
            }
            else
            {
                ViewData["Creator"] = false;
            }

            //有无创建人物
            UserCharacter userCharacter = _context.UserCharacter.AsNoTracking().Where(u => u.UserId == userid).FirstOrDefault();
            if (userCharacter == null) {
                CreateCharacter();
            }
            //有无默认场景
            if (_context.CharacterSchedule.AsNoTracking().Where(c => c.CharacterID == userCharacter.ID && c.IsMain).FirstOrDefault() != null) {
                DefaultLocation();
            }

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
                    .Join(_context.CharacterSchedule, a => a.ID, b => b.CharacterID, (a, b) => new { storycardID = b.StoryCardID,b.IsMain })
                    .FirstOrDefault(b=>b.IsMain==true);
                ReadList = _context.StoryCard.AsNoTracking().Include(a=>a.StoryOptions).AsNoTracking().SingleOrDefault(a => a.ID == storycardID.storycardID);
                //去除不符合显示条件的选项
                List<StoryOption> storyOptions = new List<StoryOption>();
                foreach (var option in ReadList.StoryOptions) {
                    if (Check(option.Condition, ReadList.StorySeriesID.ToString())) {
                        storyOptions.Add(option);
                    }
                }
                ReadList.StoryOptions = storyOptions;
                ViewBag.ReadList = ReadList;
            }
            

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> NextCard(Guid optionID)
        {
            var userid = GetUserId();

            //查看是否新建角色
            int type = new DefaultValue().CheckCharacterType(optionID);
            if (type != 0) {
                await CreateNewCharacterAsync(userid, type);
            }

            var Schedule = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == userid)
                .Join(_context.CharacterSchedule, a => a.ID,b=>b.CharacterID,(a,b)=>new { IsStory = b.IsStory, StorySeriesID=b.StorySeriesID, StoryCardID =b.StoryCardID })
                .FirstOrDefault();
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

                    Execute(opt.Effect);

                    UpdateCharacterSchedule(userid, opt);

                    return Json(card);
                }
            }
            else
            {
                //地图场景处理
            }
            return Json("不存在的后续故事卡片！");
        }
        /// <summary>
        /// 更新角色当前故事卡进度
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="opt"></param>
        private void UpdateCharacterSchedule(Guid userid, StoryOption opt)
        {
            Guid CharacterID = _context.UserCharacter.AsNoTracking().Where(a => a.UserId == userid).FirstOrDefault().ID;
            var characterSchedule = _context.CharacterSchedule.Where(a => a.CharacterID == CharacterID).FirstOrDefault();
            characterSchedule.StoryCardID = opt.NextStoryCardID;
            _context.Update(characterSchedule);
            _context.SaveChanges();
        }

        /// <summary>
        /// 检查是否符合显示条件
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        private bool Check(string Condition,string StorySeriesID)
        {
            List<StoryStatus> storyStatuses = JsonConvert.DeserializeObject<List<StoryStatus>>(Condition);
            foreach (var status in storyStatuses) {
                var DBstatus = _context.StoryStatus.AsNoTracking().Where(a => a.StorySeries == StorySeriesID && a.Name == status.Name).FirstOrDefault();
                if (DBstatus != null) {
                    int dbstatus = Convert.ToInt32(DBstatus.Value);
                    int statues = Convert.ToInt32(status.Value);
                    bool result = false;
                    switch (Convert.ToInt32(status.Type)) {
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

        /// <summary>
        /// 检测在故事卡中还是在场景中
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="StorySeriesID"></param>
        /// <returns></returns>
        private bool InLocation(Guid userid, out Guid StorySeriesID)
        {
            var location = _context.UserCharacter.AsNoTracking().Where(p => p.UserId == userid)
                .Join(_context.CharacterSchedule,u => u.ID,c => c.CharacterID,(u,c)=>new { c.IsStory,c.StorySeriesID }).ToList();

            StorySeriesID = Guid.Empty;

            foreach (var state in location)
            {
                if(state.IsStory == true)
                {
                    StorySeriesID = state.StorySeriesID;
                    ViewBag.InLocation = false;
                    return false;
                }
            }
            ViewBag.InLocation = true;
            return true;
        }

        /// <summary>
        /// 执行进行选项时下一部操作
        /// </summary>
        /// <param name="Effect"></param>
        private void Execute(string Effect) {
            List<StoryStatus> storyStatuses = JsonConvert.DeserializeObject<List<StoryStatus>>(Effect);
            //暂时只能修改状态
            foreach (var obj in storyStatuses) {
                switch (Convert.ToInt32(obj.Type)) {
                    case (int)enumEffectType.增加:
                        Increase(obj);
                        break;
                    case (int)enumEffectType.减少:
                        Reduce(obj);
                        break;
                    case (int)enumEffectType.赋值:
                        Assign(obj);
                        break;
                }
            }
        }

        /// <summary>
        /// 返回创建人物故事卡
        /// </summary>
        private IActionResult CreateCharacter() {
            ViewBag.ReadList = _context.StoryCard.AsNoTracking().Include(a => a.StoryOptions).AsNoTracking().SingleOrDefault(a => a.ID == new DefaultValue().createCharacter);
            ViewBag.InLocation = false;   
            return View();
        }

        /// <summary>
        /// 执行创建人物 ,设置为主要角色,并返回主界面
        /// </summary>
        /// <param name="userid"></param>
        private async Task CreateNewCharacterAsync(Guid userid, int characterType) {
            Guid DefaultLocation = new DefaultValue().characterNextCard(characterType);//默认地点
            switch (characterType) {
                case (int)enumCharacterType.作家:
                    //作者角色
                    UserCharacter character = new UserCharacter() {
                        ID = Guid.NewGuid(),
                        UserId = userid,
                        CharacterName = string.Empty,
                    };
                    //赋予默认地点卡片
                    
                    //记录日程表
                    CharacterSchedule schedule = new CharacterSchedule()
                    {
                        ID = Guid.NewGuid(),
                        CharacterID = character.ID,
                        StorySeriesID  = DefaultLocation,
                        StoryCardID = Guid.Empty,
                        IsMain = true,
                        IsStory = false,
                    };
                    _context.UserCharacter.Add(character);
                    break;
            }

            await NextCard(Guid.Empty);
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

        private void Increase(StoryStatus storyStatus)
        {
            StoryStatus status = _context.StoryStatus.Where(a => a.StorySeries == storyStatus.StorySeries && a.Name == storyStatus.Name).FirstOrDefault();
            if (status != null) {
                status.Value = (Convert.ToInt32(status.Value)+Convert.ToInt32(storyStatus.Value)).ToString();
                _context.Update(status);
                _context.SaveChanges();
            }
        }
        private void Reduce(StoryStatus storyStatus)
        {
            StoryStatus status = _context.StoryStatus.Where(a => a.StorySeries == storyStatus.StorySeries && a.Name == storyStatus.Name).FirstOrDefault();
            if (status != null)
            {
                status.Value = (Convert.ToInt32(status.Value) - Convert.ToInt32(storyStatus.Value)).ToString();
                _context.Update(status);
                _context.SaveChanges();
            }
        }
        private void Assign(StoryStatus storyStatus)
        {
            StoryStatus status = _context.StoryStatus.Where(a => a.StorySeries == storyStatus.StorySeries && a.Name == storyStatus.Name).FirstOrDefault();
            if (status != null)
            {
                status.Value = storyStatus.Value;
                _context.Update(status);
                _context.SaveChanges();
            }
        }

    }

}