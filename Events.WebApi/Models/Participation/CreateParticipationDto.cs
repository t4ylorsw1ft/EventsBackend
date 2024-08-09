using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Participations.Commands.CreateParticipation;
using Events.Domain;

namespace Events.WebApi.Models.Participation
{
    public class CreateParticipationDto : IMapWith<CreateParticipationCommand>
    {

        public Guid EventId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateParticipationDto, CreateParticipationCommand>();
        }
    }
}
