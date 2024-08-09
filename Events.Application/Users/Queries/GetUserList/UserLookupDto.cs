using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Application.Events.Queries.GetEventList;
using Events.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Users.Queries.GetUserList
{
    public class UserLookupDto : IMapWith<User>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookupDto>()
                .ForMember(userDto => userDto.FullName,
                opt => opt.MapFrom(user => $"{user.Name} {user.Surname}"));
        }
    }
}
