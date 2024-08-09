using Events.Application.Common.Exceptions;
using Events.Application.Events.Commands.UpdateEvent;
using Events.Application.Interfaces;
using Events.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Events.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserCommandHandler(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;
        public async Task<Unit> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _unitOfWork.UserRepository().GetById(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            entity.Name = request.Name;
            entity.Surname = request.Surname;
            entity.BirthDate = request.BirthDate;
            entity.Email = request.Email;
            entity.Password = request.Password;

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}