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

namespace Events.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler
        : IRequestHandler<UpdateEventCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateEventCommandHandler(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;
        public async Task<Unit> Handle(UpdateEventCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _unitOfWork.EventRepository().GetById(request.Id, cancellationToken);

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.EventDateTime = request.EventDateTime;
            entity.Location = request.Location;
            entity.Category = request.Category;
            entity.MaxParticipants = request.MaxParticipants;
            entity.ImageUrl = request.ImageUrl;


            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
