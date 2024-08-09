using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Participations.Commands.CreateParticipation
{
    public class CreateParticipationCommandValidator : AbstractValidator<CreateParticipationCommand>
    {
        public CreateParticipationCommandValidator()
        {
            RuleFor(p =>
               p.EventId).NotEqual(Guid.Empty);
            RuleFor(p =>
              p.UserId).NotEqual(Guid.Empty);
        }
    }
}
