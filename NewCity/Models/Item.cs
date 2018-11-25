using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class Item
    {

        public Guid ID { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 地点坐标
        /// </summary>
        public string Coordinate { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// 效果
        /// </summary>
        public string Effect { get; set; }
    }
}
