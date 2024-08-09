using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator() 
        {
            RuleFor(updateEventCommand =>
               updateEventCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateEventCommand =>
               updateEventCommand.Description).NotEmpty().MaximumLength(1000);
            RuleFor(updateEventCommand =>
               updateEventCommand.Title).NotEmpty().MaximumLength(150);
            RuleFor(updateEventCommand =>
               updateEventCommand.EventDateTime).NotEmpty().GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(updateEventCommand =>
                updateEventCommand.Location).NotEmpty().MaximumLength(250);
            RuleFor(updateEventCommand =>
                updateEventCommand.Category).NotEmpty().MaximumLength(50);
            RuleFor(updateEventCommand =>
                updateEventCommand.MaxParticipants).NotEmpty();
            RuleFor(updateEventCommand =>
                updateEventCommand.ImageUrl).NotEmpty().MaximumLength(250);
        }
    }
}
