using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Users.Commands.UpdateUser;
using Events.Domain;

namespace Events.WebApi.Models.User
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>();
        }
    }
}
