using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Events.Commands.UpdateEvent;
using Events.Domain;

namespace Events.WebApi.Models.Event
{
    public class UpdateEventDto : IMapWith<UpdateEventCommand>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public int MaxParticipants { get; set; }
        public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateEventDto, UpdateEventCommand>();
        }
    }
}
