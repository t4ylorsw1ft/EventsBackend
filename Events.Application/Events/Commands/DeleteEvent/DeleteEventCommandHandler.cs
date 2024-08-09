using Events.Application.Common.Exceptions;
using Events.Application.Interfaces;
using Events.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Events.Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler
        : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteEventCommandHandler(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;
        public async Task<Unit> Handle(DeleteEventCommand request,
            CancellationToken cancellationToken)
        {

            await _unitOfWork.EventRepository().Delete(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
