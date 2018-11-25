using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class ItemLog
    {
        public Guid ID { get; set; }

        
        public Guid ItemID { get; set; }

        public Guid CharacterID { get; set; }

        /// <summary>
        /// 历史信息
        /// </summary>
        public string Msg { get; set; }




    }
}
