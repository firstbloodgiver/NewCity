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
        /// 地点介绍 
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 地点坐标
        /// </summary>
        public string Coordinate { get; set; }
  
        
    }
}
