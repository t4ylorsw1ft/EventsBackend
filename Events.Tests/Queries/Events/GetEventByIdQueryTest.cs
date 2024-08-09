using AutoMapper;
using Events.Application.Common.Exceptions;
using Events.Application.Events.Queries.GetEventDetails;
using Events.Application.Users.Queries.GetUserList;
using Events.Domain;
using Events.Persistence;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Events.Tests.Queries.Events
{
    public class GetEventByIdQueryTest : TestQueryBase
    {
        [Fact]
        public async Task GetEventDetails()
        {
            var handler = new GetEventDetailsQueryHandler(unitOfWork, mapper);

            var result = await handler.Handle(
                new GetEventDetailsQuery { Id = EventsContextFactory.EventIdForUpdate },
                CancellationToken.None);

            Assert.IsType<EventDetailsVm>(result);
            Assert.True(
                result.Id == EventsContextFactory.EventIdForUpdate &&
                result.Title == "City Marathon - Charity Run" &&
                result.Description == "Participate in our annual marathon for charity." &&
                result.EventDateTime == new DateTime(2024, 10, 5, 7, 30, 0) &&
                result.Location == "Central Park, New York" &&
                result.Category == "Sports" &&
                result.MaxParticipants == 3 &&
                result.ImageUrl == "https://example.com/images/marathon.jpg"
                );
            Assert.Equal(result.Participants.Count, 2);
        }

        [Fact]
        public async Task GetEventDetails_NotFound()
        {
            var handler = new GetEventDetailsQueryHandler(unitOfWork, mapper);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new GetEventDetailsQuery { Id = Guid.NewGuid() },
                CancellationToken.None));
        }
    }
}
