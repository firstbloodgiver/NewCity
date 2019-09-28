using NewCity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class UserSchedule
    {

        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid CharacterID { get; set; }
        public Guid StorySeriesID { get; set; }
        public Guid StoryCardID { get; set; }

        public enumStoryStatus ScheduleStatus { get; set; }
        /// <summary>
        /// 是否在主页面
        /// </summary>
        /// <returns></returns>
        public bool IsMain { get; set; }

        /// <summary>
        /// 是故事就读取StoryCardID 地点就读取StorySeriesID
        /// </summary>
        public bool IsStory { get; set; }

    }
}
