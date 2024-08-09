using Events.Domain;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.ParticipationRepositoryTests
{
    public class CreateParticipationTest : TestRepositoryBase
    {
        [Fact]
        public async Task CreateParticipation()
        {
            var rep = new ParticipationRepository(context);
            var participation = new Participation
            {
                Id = Guid.NewGuid(),
                RegistrationDateTime = new DateTime(1990, 4, 10),
                EventId = EventsContextFactory.EventIdForUpdate,
                UserId = EventsContextFactory.UserAId
            };

            await rep.Create(participation, CancellationToken.None);
            await context.SaveChangesAsync();

            Assert.NotNull(await context.Participations.SingleOrDefaultAsync(p =>
                p.Id == participation.Id &&
                p.RegistrationDateTime == participation.RegistrationDateTime &&
                p.EventId == participation.EventId &&
                p.UserId == participation.UserId
                ));
        }

        [Fact]
        public async Task CreateParticipation_AlreadyExists()
        {
            var rep = new ParticipationRepository(context);
            var participation = new Participation
            {
                Id = Guid.NewGuid(),
                RegistrationDateTime = new DateTime(1990, 4, 10),
                EventId = EventsContextFactory.EventIdForUpdate,
                UserId = EventsContextFactory.UserBId
            };

            await Assert.ThrowsAsync<Exception>(async () => await rep.Create(participation, CancellationToken.None));
        }
    }
}
