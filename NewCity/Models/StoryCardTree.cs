using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class StoryCardTree
    {
        public Guid FatherStoryCardId { get; set; }
        public int Level { get; set; }
        public StoryCard StoryCard = new StoryCard();
    }

    public class StoryCardTrees
    {
        
        public int Level { get; set; }
        public List<StoryCardTree> StoryCard = new List<StoryCardTree>();
    }
}
