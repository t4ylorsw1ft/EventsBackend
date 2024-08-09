using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Auth.Logout
{
    public class LogoutCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
