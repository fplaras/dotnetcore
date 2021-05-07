using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsModule.Models
{
    public class HackerNewsStories
    {
        public List<int> NewStories { get; set; }
        public List<int> TopStories { get; set; }
        public List<int> BestStories { get; set; }
        public List<int> AskStories { get; set; }
        public List<int> JobStories { get; set; }
        public List<int> ShowStories { get; set; }
    }
}
