using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace viewcomponent_samples.Models
{
    public class EventDTO
    {
        public int EventId { get; set; }
        public List<EventReviewDTO> Reviews { get; set; }
    }
}
