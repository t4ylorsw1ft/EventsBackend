using Events.Application.Common.Exceptions;
using Events.Application.Events.Commands.DeleteEvent;
using Events.Application.Interfaces;
using Events.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Participations.Commands.DeleteParticipation
{
    public class DeleteParticipationCommandHandler
        : IRequestHandler<DeleteParticipationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteParticipationCommandHandler(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;
        public async Task<Unit> Handle(DeleteParticipationCommand request,
            CancellationToken cancellationToken)
        {

            await _unitOfWork.ParticipationRepository().Delete(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
