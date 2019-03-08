using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class Location
    {
        
        public Guid ID { get; set; }

        /// <summary>
        /// 地点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地点图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 地点介绍 
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 地点坐标
        /// </summary>
        public string Coordinate { get; set; }
  
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 地点的故事系列
        /// </summary>
        public ICollection<StorySeries> StorySeries { get; set; }
    }
}
