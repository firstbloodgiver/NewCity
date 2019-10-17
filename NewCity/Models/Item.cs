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
        /// 所属故事系列
        /// </summary>
        public Guid StorySeriesID { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// 效果1
        /// </summary>
        public string EffectName1 { get; set; }
        /// <summary>
        /// 效果1
        /// </summary>
        public string EffectType1 { get; set; }
        /// <summary>
        /// 效果1
        /// </summary>
        public string EffectValue1 { get; set; }
        /// <summary>
        /// 效果2
        /// </summary>
        public string EffectName2 { get; set; }
        /// <summary>
        /// 效果2
        /// </summary>
        public string EffectType2 { get; set; }
        /// <summary>
        /// 效果2
        /// </summary>
        public string EffectValue2 { get; set; }
        /// <summary>
        /// 效果3
        /// </summary>
        public string EffectName3 { get; set; }
        /// <summary>
        /// 效果3
        /// </summary>
        public string EffectType3 { get; set; }
        /// <summary>
        /// 效果3
        /// </summary>
        public string EffectValue3 { get; set; }
        /// <summary>
        /// 效果4
        /// </summary>
        public string EffectName4 { get; set; }
        /// <summary>
        /// 效果4
        /// </summary>
        public string EffectType4 { get; set; }
        /// <summary>
        /// 效果4
        /// </summary>
        public string EffectValue4 { get; set; }
        /// <summary>
        /// 效果5
        /// </summary>
        public string EffectName5 { get; set; }
        /// <summary>
        /// 效果5
        /// </summary>
        public string EffectType5 { get; set; }
        /// <summary>
        /// 效果5
        /// </summary>
        public string EffectValue5 { get; set; }
    }
}
