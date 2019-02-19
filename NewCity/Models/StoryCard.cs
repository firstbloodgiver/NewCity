using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    
    public class StoryCard
    {
        
        public Guid ID { get; set; }
        public Guid StorySeriesID { get; set; }
        public string StoryName { get; set; }

        public string Text { get; set; }
        public string IMG { get; set; }
        public string BackgroundIMG { get; set; }

        public ICollection<StoryOption> StoryOptions { get; set; }
    }
}
