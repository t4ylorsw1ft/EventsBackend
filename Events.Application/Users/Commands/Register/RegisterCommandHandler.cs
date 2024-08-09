using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Interfaces;
using Events.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Events.Application.Users.Commands.Register
{
    public class RegisterCommandHandler
          : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) =>
            (_unitOfWork, _passwordHasher) = (unitOfWork, passwordHasher);

        public async Task<Guid> Handle(RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Surname = request.Surname,
                BirthDate = request.BirthDate,
                Email = request.Email,
                Password = _passwordHasher.Generate(request.Password),
                IsAdmin = false,
                Participations = new List<Participation>()
            };

            Console.WriteLine(user.Name);

            await _unitOfWork.UserRepository().Create(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }
}
