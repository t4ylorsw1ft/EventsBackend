using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Queries.GetEventDetails
{
    public class ParticipationEventDto : IMapWith<Participation>
    {
        public Guid ParticipationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Participation, ParticipationEventDto>()
                .ForMember(dest => dest.ParticipationId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Surname,
                    opt => opt.MapFrom(src => src.User.Surname))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User.Email));
        }
    }
}
