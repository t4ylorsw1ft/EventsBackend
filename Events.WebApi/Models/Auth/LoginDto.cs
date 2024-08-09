using AutoMapper;
using Events.Application.Auth.Login;
using Events.Application.Common.Mappings;

namespace Events.WebApi.Models.Auth
{
    public class LoginDto : IMapWith<LoginCommand>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDto, LoginCommand>();
        }
    }
}
