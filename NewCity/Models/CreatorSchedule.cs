using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class CreatorSchedule
    {

        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid StorySeriesID { get; set; }
        public Guid StoryCardID { get; set; }

    }
}
