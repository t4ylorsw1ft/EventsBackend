using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Participations.Commands.DeleteParticipation
{
    public class DeleteParticipationCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
