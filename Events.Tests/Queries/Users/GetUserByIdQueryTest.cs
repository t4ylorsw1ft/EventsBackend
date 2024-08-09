using AutoMapper;
using Events.Application.Common.Exceptions;
using Events.Application.Events.Queries.GetEventDetails;
using Events.Application.Users.Queries.GetUserDetails;
using Events.Domain;
using Events.Persistence;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Events.Tests.Queries.Users
{
    public class GetUserByIdQueryTest : TestQueryBase
    {
        [Fact]
        public async Task GetUserDetails()
        {
            var handler = new GetUserDetailsQueryHandler(unitOfWork, mapper);

            var result = await handler.Handle(
                new GetUserDetailsQuery { Id = EventsContextFactory.UserBId },
                CancellationToken.None);

            Assert.IsType<UserDetailsVm>(result);
            Assert.True(
                result.Id == EventsContextFactory.UserBId &&
                result.Name == "Bob" &&
                result.Surname == "Smith" &&
                result.BirthDate == new DateTime(1985, 12, 20) &&
                result.Email == "bob.smith@example.com"
            );
            Assert.Equal(result.Participations.Count, 1);
        }

        [Fact]
        public async Task GetEventDetails_NotFound()
        {
            var handler = new GetUserDetailsQueryHandler(unitOfWork, mapper);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new GetUserDetailsQuery { Id = Guid.NewGuid() },
                CancellationToken.None));
        }
    }
}
