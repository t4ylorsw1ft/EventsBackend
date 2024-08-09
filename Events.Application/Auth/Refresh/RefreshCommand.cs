using Events.Persistence.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Auth.Refresh
{
    public class RefreshCommand : IRequest<JwtTokensDto>
    {
        public string RefreshToken { get; set; }
    }
}
