using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Queries.GetEventList
{
    public class EventListVm
    {
        public IList<EventLookupDto> Events { get; set; }
    }
}
