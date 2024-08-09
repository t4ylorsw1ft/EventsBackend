using Events.Application.Common.Exceptions;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.EventRepositoryTests
{
    public class DeleteEventTest : TestRepositoryBase
    {
        [Fact]
        public async Task DeleteEvent()
        {
            var rep = new EventRepository(context);
            await rep.Delete(EventsContextFactory.EventIdForDelete, CancellationToken.None);
            await context.SaveChangesAsync();
            Assert.Null(context.Events.SingleOrDefault(eventt =>
                eventt.Id == EventsContextFactory.EventIdForDelete));
        }

        [Fact]
        public async Task DeleteEvent_NotFound()
        {
            var rep = new EventRepository(context);
            await Assert.ThrowsAsync<NotFoundException>(async () => await rep.Delete(Guid.NewGuid(), CancellationToken.None));
        }
    }
}