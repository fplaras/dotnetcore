using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsModule.Models
{
    public class HackerNewsSearchOptions
    {
        public bool IncludeNewStories { get; set; }
        public bool IncludeTopStories { get; set; }
        public bool IncludeBestStories { get; set; }
        public bool IncludeAskStories { get; set; }
        public bool IncludeJobStories { get; set; }
        public bool IncludeShowStories { get; set; }
    }
}
