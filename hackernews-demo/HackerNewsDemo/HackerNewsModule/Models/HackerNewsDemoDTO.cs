using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HackerNewsModule.Models
{
    public class HackerNewsDemoDTO
    {
        public string By { get; set; }

        public int Descendants { get; set; }

        public int Id { get; set; }

        public int Score { get; set; }

        public int Time { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }
        public string Text { get; set; }

        public List<HackerNewsDemoDTO> Kids { get; set; }

        public List<HackerNewsDemoDTO> Parts { get; set; }
    }
}
