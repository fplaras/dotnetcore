using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsModule.Domain
{
    public partial  class HackerNewsItemDomain
    {
        public HackerNewsItemDomain()
        {

        }

        [Key]
        public int Id { get; set; }

        public string By { get; set; }

        public int Descendants { get; set; }

        public int Score { get; set; }

        public int Time { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public bool Deleted { get; set; }
        public string Kids { get; set; }
        public string Parts { get; set; }
        public string Text { get; set; }

    }
}
