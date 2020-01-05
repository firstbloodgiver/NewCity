using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class UserCharacter
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色名称 
        /// </summary>
        public string CharacterName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActivate { get; set; }
       

        //////////////////////////////////////////基础能力////////////////////////////////////////////////////////
        
        /// <summary>
        /// 健康最大值
        /// </summary>
        public float MaxHealthy { get; set; }

        /// <summary>
        /// 健康程度
        /// </summary>
        public float Healthy { get; set; }

        /// <summary>
        /// 神智最大值
        /// </summary>
        public float MaxSanity { get; set; }

        /// <summary>
        /// 神智程度
        /// </summary>
        public float Sanity { get; set; }

        /// <summary>
        /// 行动力
        /// 可以进行多少消耗精力的工作
        /// </summary>
        public float ActionPoints { get; set; }

        /// <summary>
        /// 幸运 骰子
        /// </summary>
        public float Lucky { get; set; }

        /// <summary>
        /// 速度\敏捷
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        public float Strength { get; set; }

        /// <summary>
        /// 智力,学习
        /// </summary>
        public float Intelligence { get; set; }

        //////////////////////////////////////////社会能力////////////////////////////////////////////////////////


        /// <summary>
        /// 社会经验 - 骰子有机会知道对方意图,搜索能力
        /// </summary> 
        public float Experience { get; set; }

        /// <summary>
        /// 社会地位 - 说服
        /// </summary>
        public float Status { get; set; }

        /// <summary>
        /// 善恶值
        /// </summary>
        public float Moral { get; set; }




    }
}
