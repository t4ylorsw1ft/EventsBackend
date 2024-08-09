using Events.Application.Events.Commands.DeleteEvent;
using Events.Application.Events.Commands.UpdateEvent;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Commands.Events
{
    public class UpdateEventCommandTest : TestCommandBase
    {
        [Fact]
        public async Task UpdateEvent()
        {
            var handler = new UpdateEventCommandHandler(unitOfWork);

            string title = "Wrestrling Competitions";
            string description = "Best wrestlers from all over the world fighting for the champion title";
            DateTime eventDateTime = new DateTime(2024, 9, 20, 20, 30, 0);
            string location = "Minsk-Arena, Minsk";
            string category = "Sport";
            int maxParticipants = 4;
            string imageUrl = "https://example.com/images/wrestling.jpg";

            var command = new UpdateEventCommand()
            {
                Id = EventsContextFactory.EventIdForUpdate,
                Title = title,
                Description = description,
                EventDateTime = eventDateTime,
                Location = location,
                Category = category,
                MaxParticipants = maxParticipants,
                ImageUrl = imageUrl
            };


            await handler.Handle(command, CancellationToken.None);
            await context.SaveChangesAsync();


            Assert.NotNull(await context.Events.SingleOrDefaultAsync(e =>
                e.Id == EventsContextFactory.EventIdForUpdate &&
                e.Title == title &&
                e.Description == description &&
                e.EventDateTime == eventDateTime &&
                e.Location == location &&
                e.Category == category &&
                e.MaxParticipants == maxParticipants &&
                e.ImageUrl == imageUrl));
        }
    }
}
