using AutoMapper;
using AutoMapper.QueryableExtensions;
using Events.Application.Interfaces;
using Events.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Events.Queries.GetEventList
{
    public class GetEventListQueryHandler
        :IRequestHandler<GetEventListQuery,EventListVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventListQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<EventListVm> Handle(GetEventListQuery request,
            CancellationToken cancellationToken)
        {
            var eventsQuery = await _unitOfWork.EventRepository().GetAll();

            if(!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                eventsQuery = eventsQuery
                    .Where(e => e.Title.ToLower().Contains(request.SearchTerm.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                eventsQuery = eventsQuery
                    .Where(e => e.Category == request.Category);
            }
            if (!string.IsNullOrWhiteSpace(request.Location))
            {
                eventsQuery = eventsQuery
                    .Where(e => e.Location == request.Location);
            }
            if (request.EventDateTime.HasValue)
            {
                eventsQuery = eventsQuery
                    .Where(e => e.EventDateTime == request.EventDateTime);
            }

            var projectedEvents = await eventsQuery
                .ProjectTo<EventLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new EventListVm {Events = projectedEvents };
        }

    }
}
