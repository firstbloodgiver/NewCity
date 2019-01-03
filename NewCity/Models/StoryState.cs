using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    /// <summary>
    /// 记录故事系列的状态
    /// </summary>
    public class StoryState
    {

        public Guid ID { get; set; }

        public string StorySeries { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
