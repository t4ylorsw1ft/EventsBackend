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

namespace Events.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler
        : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) =>
            (_unitOfWork, _passwordHasher) = (unitOfWork, passwordHasher);
        public async Task<Unit> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository().GetById(request.Id, cancellationToken);
            var verification = _passwordHasher.Verify(request.Password, user.Password);

            if (verification == false)
            {
                throw new Exception("Неверный пароль");
            }
            Console.WriteLine($"id: {request.Id}");
            await _unitOfWork.UserRepository().Delete(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
