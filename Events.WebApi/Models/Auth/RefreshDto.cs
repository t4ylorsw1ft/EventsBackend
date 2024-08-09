using AutoMapper;
using Events.Application.Auth.Login;
using Events.Application.Auth.Refresh;
using Events.Application.Common.Mappings;

namespace Events.WebApi.Models.Auth
{
    public class RefreshDto : IMapWith<RefreshCommand>
    {
        public string RefreshToken { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RefreshDto, RefreshCommand>();
        }
    }
}
