using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Participations.Commands.CreateParticipation;
using Events.Domain;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Commands.Participations
{
    public class CreateParticipationCommandTest : TestCommandBase
    {
        [Fact]
        public async Task CreateParticipation()
        {
            var handler = new CreateParticipationCommandHandler(unitOfWork);

            var command = new CreateParticipationCommand
            {
                EventId = EventsContextFactory.EventIdForUpdate,
                UserId = EventsContextFactory.UserAId
            };
            var participationId = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(await context.Participations.SingleOrDefaultAsync(p =>
                p.Id == participationId &&
                p.EventId == EventsContextFactory.EventIdForUpdate &&
                p.UserId == EventsContextFactory.UserAId
                ));
        }

        [Fact]
        public async Task CreateParticipation_AlreadyExists()
        {
            var handler = new CreateParticipationCommandHandler(unitOfWork);

            var command = new CreateParticipationCommand
            {
                EventId = EventsContextFactory.EventIdForUpdate,
                UserId = EventsContextFactory.UserBId
            };

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
