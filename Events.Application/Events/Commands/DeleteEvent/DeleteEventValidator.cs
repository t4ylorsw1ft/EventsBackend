using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Commands.DeleteEvent
{
    public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventValidator() 
        {
            RuleFor(deleteEventCommand =>
                deleteEventCommand.Id).NotEqual(Guid.Empty);
        }

    }
}
