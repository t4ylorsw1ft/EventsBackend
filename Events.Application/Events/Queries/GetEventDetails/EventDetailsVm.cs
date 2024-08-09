using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Application.Events.Queries.GetEventList;
using Events.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Queries.GetEventDetails
{
    public class EventDetailsVm : IMapWith<Event>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public int MaxParticipants { get; set; }
        public List<ParticipationEventDto> Participants { get; set; }
        public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<Event, EventDetailsVm>()
                 .ForMember(dest => dest.Participants, 
                 opt => opt.MapFrom(src => src.Participations));

        }
    }
}
