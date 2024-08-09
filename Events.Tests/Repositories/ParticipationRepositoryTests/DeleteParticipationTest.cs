using Events.Application.Common.Exceptions;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.ParticipationRepositoryTests
{
    public class DeleteParticipationTest : TestRepositoryBase
    {
        [Fact]
        public async Task DeleteParticipation()
        {
            var rep = new ParticipationRepository(context);
            await rep.Delete(EventsContextFactory.ParticipationAId, CancellationToken.None);
            await context.SaveChangesAsync();
            Assert.Null(context.Participations.SingleOrDefault(eventt =>
                eventt.Id == EventsContextFactory.ParticipationAId));
        }

        [Fact]
        public async Task DeleteParticipation_NotFound()
        {
            var rep = new ParticipationRepository(context);
            await Assert.ThrowsAsync<NotFoundException>(async () => await rep.Delete(Guid.NewGuid(), CancellationToken.None));
        }
    }
}