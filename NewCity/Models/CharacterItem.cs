using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class CharacterItem
    {
        public Guid ID { get; set; }

        public bool Special { get; set; }

        public Guid ItemID { get; set; }

        public Guid CharacterID { get; set; }

        /// <summary>
        /// 个数
        /// </summary>
        public int Amount { get; set; }

    }
}
