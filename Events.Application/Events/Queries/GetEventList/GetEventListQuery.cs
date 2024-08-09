using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Queries.GetEventList
{
    public class GetEventListQuery : IRequest<EventListVm>
    {
        public string? SearchTerm { get; set; }
        public string? Category { get; set;}
        public string? Location { get; set; }
        public DateTime? EventDateTime { get; set; }

    }
}
