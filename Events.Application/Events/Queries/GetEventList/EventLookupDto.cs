using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Queries.GetEventList
{
    public class EventLookupDto : IMapWith<Event>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public int ParticipantsLeft { get; set; }
        public int MaxParticipants { get; set; }
        public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventLookupDto>()
                .ForMember(eventDto => eventDto.ParticipantsLeft,
                opt => opt.MapFrom(eventt => eventt.MaxParticipants - (int) eventt.Participations.Count));
        }
    }

}
