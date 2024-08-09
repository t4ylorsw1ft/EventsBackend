using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Interfaces;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events.Application.Auth.Logout
{
    public class LogoutCommandHandler
        : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LogoutCommandHandler(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;
        public async Task<Unit> Handle(LogoutCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository().GetById(request.Id, cancellationToken);

            user.RefreshToken = "";

            await _unitOfWork.UserRepository().Update(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
