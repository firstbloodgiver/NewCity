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
        /// 角色故事进度
        /// </summary>
        public ICollection<CharacterSchedule> CharacterSchedules { get; set; }

    }
}
