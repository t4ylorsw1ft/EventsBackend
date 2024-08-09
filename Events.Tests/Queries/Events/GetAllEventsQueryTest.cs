using AutoMapper;
using Events.Application.Events.Queries.GetEventList;
using Events.Persistence;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Queries.Events
{
    public class GetAllEventsQueryTest : TestQueryBase
    {
        [Fact]
        public async Task GetEventListQuery()
        {
            var handler = new GetEventListQueryHandler(unitOfWork, mapper);

            var result = await handler.Handle(
                new GetEventListQuery(),
                CancellationToken.None);

            Assert.IsType<EventListVm>(result);
            Assert.True(result.Events.Count == 4);
        }
    }
}
