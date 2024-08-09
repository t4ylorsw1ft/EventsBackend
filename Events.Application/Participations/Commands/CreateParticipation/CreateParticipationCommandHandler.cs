using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Interfaces;
using Events.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Participations.Commands.CreateParticipation
{
    public class CreateParticipationCommandHandler
        : IRequestHandler<CreateParticipationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateParticipationCommandHandler(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;
        public async Task<Guid> Handle(CreateParticipationCommand request,
            CancellationToken cancellationToken)
        {
            var participation = new Participation
            {
                Id = Guid.NewGuid(),
                RegistrationDateTime = DateTime.Now,
                EventId = request.EventId,
                Event = request.Event,
                UserId = request.UserId,
                User = request.User
            };



            await _unitOfWork.ParticipationRepository().Create(participation, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return participation.Id;
        }
    }
}

