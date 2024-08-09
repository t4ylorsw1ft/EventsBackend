using AutoMapper;
using Events.Application.Common.Exceptions;
using Events.Application.Interfaces;
using Events.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Events.Application.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQueryHandler
            : IRequestHandler<GetEventDetailsQuery, EventDetailsVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventDetailsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<EventDetailsVm> Handle(GetEventDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.EventRepository().GetById(request.Id, cancellationToken);
            return _mapper.Map<EventDetailsVm>(entity);
        }
    }
}
