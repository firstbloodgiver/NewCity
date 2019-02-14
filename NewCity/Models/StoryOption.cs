using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class StoryOption
    {
        public Guid ID { get; set; }
        public Guid StoryCardID { get; set; }


        public string Text { get; set; }
        public Guid NextStoryCardID { get; set; } 

        /// <summary>
        /// 选项出现条件
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 选项点击后效果
        /// </summary>
        public string Effect { get; set; }
    }
}
