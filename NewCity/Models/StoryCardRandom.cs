using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class StoryCardRandom
    {
        public Guid ID { get; set; }
        public Guid StorySeriesID { get; set; }
        public Guid StoryCardID { get; set; }
        public int Value { get; set; }
        public Guid UserID { get; set; }

    }
}
