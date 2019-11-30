using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class CharacterStatus
    {
        public Guid UserID { get; set; }

        public Guid ID { get; set; }

        public string StorySeries { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
