using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidatior : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidatior() 
        {
            RuleFor(createEventCommand =>
               createEventCommand.Title).NotEmpty().MaximumLength(150);
            RuleFor(createEventCommand =>
               createEventCommand.Description).NotEmpty().MaximumLength(1000);
            RuleFor(createEventCommand =>
               createEventCommand.EventDateTime).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(createEventCommand =>
                createEventCommand.Location).NotEmpty().MaximumLength(250);
            RuleFor(createEventCommand =>
                createEventCommand.Category).NotEmpty().MaximumLength(50);
            RuleFor(createEventCommand =>
                createEventCommand.MaxParticipants).NotEmpty();
            RuleFor(createEventCommand =>
                createEventCommand.ImageUrl).NotEmpty().MaximumLength(250);
        }
       
    }
}
