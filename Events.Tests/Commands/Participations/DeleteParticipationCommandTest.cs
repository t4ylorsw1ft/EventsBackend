using Events.Application.Common.Exceptions;
using Events.Application.Participations.Commands.CreateParticipation;
using Events.Application.Participations.Commands.DeleteParticipation;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Commands.Participations
{
    public class DeleteParticipationCommandTest : TestCommandBase
    {
        [Fact]
        public async Task DeleteParticipation()
        {
            var handler = new DeleteParticipationCommandHandler(unitOfWork);

            var command = new DeleteParticipationCommand
            {
                Id = EventsContextFactory.ParticipationBId,
            };

            await handler.Handle(command, CancellationToken.None);

            Assert.Null(context.Participations.SingleOrDefault(part =>
                part.Id == EventsContextFactory.ParticipationBId));
        }

        [Fact]
        public async Task DeleteParticipation_NotFound()
        {
            var handler = new DeleteParticipationCommandHandler(unitOfWork);

            var command = new DeleteParticipationCommand
            {
                Id = Guid.NewGuid()
            };

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
