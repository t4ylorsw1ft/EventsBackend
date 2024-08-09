using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Application.Users.Commands.Register;

namespace Events.WebApi.Models.Auth
{
    public class RegisterDto : IMapWith<RegisterCommand>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterDto, RegisterCommand>();
        }
    }
}
