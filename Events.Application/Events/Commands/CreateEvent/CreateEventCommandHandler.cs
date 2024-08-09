using Events.Application.Interfaces;
using Events.Domain;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler
        : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateEventCommandHandler(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;
        public async Task<Guid> Handle(CreateEventCommand request,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("CreateHandler");
            var eventt = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                EventDateTime = request.EventDateTime,
                Location = request.Location,
                Category = request.Category,
                MaxParticipants = request.MaxParticipants,
                Participations = new List<Participation>(),
                ImageUrl = request.ImageUrl
            };

            await _unitOfWork.EventRepository().Create(eventt, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return eventt.Id;
        }
    }
}
