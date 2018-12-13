using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class StorySeries
    {
        public Guid ID {get;set;}
        public Guid LocationID { get; set; }
        public string SeriesName { get; set; }

        public Guid Author { get; set; }
        public DateTime Creationdate { get; set; }

        public string IMG { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// 出现条件
        /// </summary>
        public string flag { get; set; }

        /// <summary>
        /// 是否已经游玩
        /// </summary>
        public bool IsPlayed { get; set; }

        /// <summary>
        /// 是否测试
        /// </summary>
        public bool IsTest { get; set; }

        public ICollection<StoryCard> StoryCards { get; set; }
    }
}
