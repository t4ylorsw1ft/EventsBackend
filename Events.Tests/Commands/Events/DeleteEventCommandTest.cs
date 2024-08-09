using Events.Application.Common.Exceptions;
using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Events.Commands.DeleteEvent;
using Events.Persistence;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Commands.Events
{
    public class DeleteEventCommandTest : TestCommandBase
    {


        [Fact]
        public async Task DeleteEventCommand_Success()
        {
            var handler = new DeleteEventCommandHandler(unitOfWork);

            await handler.Handle(
                new DeleteEventCommand
                {
                    Id = EventsContextFactory.EventIdForDelete
                }, 
                CancellationToken.None);
            Assert.Null(context.Events.SingleOrDefault(eventt =>
                eventt.Id == EventsContextFactory.EventIdForDelete));
        }

        [Fact]
        public async Task DeleteEvent_NotFound()
        {
            var handler = new DeleteEventCommandHandler(unitOfWork);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new DeleteEventCommand
                {
                    Id =  Guid.NewGuid()
                },
                CancellationToken.None));
        }

    }
}
