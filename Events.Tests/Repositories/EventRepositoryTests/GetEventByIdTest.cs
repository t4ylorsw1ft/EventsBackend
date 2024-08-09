using Events.Application.Common.Exceptions;
using Events.Domain;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.EventRepositoryTests
{
    public class GetEventByIdTest : TestRepositoryBase
    {
        [Fact]
        public async Task GetEventById()
        {
            var rep = new EventRepository(context);

            var result = await rep.GetById(new Guid("22222222-2222-2222-2222-222222222222"), CancellationToken.None);


            Assert.NotNull(result);
            Assert.Equal(new Guid("22222222-2222-2222-2222-222222222222"), result.Id);
            Assert.Equal("Tech Conference 2024 - Session 1", result.Title);
            Assert.Equal("Explore the latest in technology and innovation.", result.Description);
            Assert.Equal(new DateTime(2024, 9, 10, 9, 0, 0), result.EventDateTime);
            Assert.Equal("Silicon Valley, California", result.Location);
            Assert.Equal("Technology", result.Category);
            Assert.Equal(3, result.MaxParticipants);
            Assert.Equal("https://example.com/images/tech_conference.jpg", result.ImageUrl);
        }
        [Fact]
        public async Task GetEventById_Failure()
        {
            var rep = new EventRepository(context);
            await Assert.ThrowsAsync<NotFoundException>(async () => await rep.GetById(Guid.NewGuid(), CancellationToken.None));
        }
    }
}
