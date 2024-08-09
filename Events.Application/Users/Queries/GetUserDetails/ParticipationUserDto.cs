using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Application.Events.Queries.GetEventDetails;
using Events.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Users.Queries.GetUserDetails
{
    public class ParticipationUserDto : IMapWith<Participation>
    {
        public Guid ParticipationId { get; set; }
        public Guid EventId { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string Title { get; set; }
        public DateTime EventDateTime { get; set; }
        public int ParticipantsLeft { get; set; }
        public int MaxParticipants { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Participation, ParticipationUserDto>()
                .ForMember(dest => dest.ParticipationId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Event.Title))
                .ForMember(dest => dest.EventDateTime,
                    opt => opt.MapFrom(src => src.Event.EventDateTime))
                .ForMember(dest => dest.ParticipantsLeft,
                    opt => opt.MapFrom(src => src.Event.MaxParticipants - (int)src.Event.Participations.Count))
                .ForMember(dest => dest.MaxParticipants,
                    opt => opt.MapFrom(src => src.Event.MaxParticipants));
        }
    }
}
