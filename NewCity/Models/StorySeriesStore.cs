using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class StorySeriesStore: StorySeries
    {
        /// <summary>
        /// 已经添加过到账号
        /// </summary>
        public bool HadAdd { get; set; } = false;
    }
}
