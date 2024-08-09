using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Participations.Commands.DeleteParticipation
{
    public class DeleteParticipationCommandValidator : AbstractValidator<DeleteParticipationCommand>
    {
        public DeleteParticipationCommandValidator()
        {
            RuleFor(p =>
                p.Id).NotEqual(Guid.Empty);
        }
    }
}
