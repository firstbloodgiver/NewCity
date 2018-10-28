using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class StorySeries
    {
        public Guid ID {get;set;}
        public string SeriesName { get; set; }

        public Guid Author { get; set; }
        public DateTime Creationdate { get; set; }

        public ICollection<StoryCard> StoryCards { get; set; }
    }
}
