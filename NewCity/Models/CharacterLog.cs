﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class CharacterLog
    {
        public Guid ID { get; set; }

        /// <summary>
        /// 经历的故事
        /// </summary>
        public Guid StoryID { get; set; }


        public Guid CharacterID { get; set; }

        /// <summary>
        /// 历史信息
        /// </summary>
        public string Msg { get; set; }



        /// <summary>
        /// 事件时间
        /// </summary>
        public DateTime CreateTime { get; set; }


    }
}
