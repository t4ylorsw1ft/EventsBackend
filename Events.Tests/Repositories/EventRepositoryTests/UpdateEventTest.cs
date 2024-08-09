using Events.Application.Common.Exceptions;
using Events.Domain;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.EventRepositoryTests
{
    public class UpdateEventTest : TestRepositoryBase
    {
        [Fact]
        public async Task UpdateEvent()
        {
            string title = "Wrestrling Competitions";
            string description = "Best wrestlers from all over the world fighting for the champion title";
            DateTime eventDateTime = new DateTime(2024, 9, 20, 20, 30, 0);
            string location = "Minsk-Arena, Minsk";
            string category = "Sport";
            int maxParticipants = 4;
            string imageUrl = "https://example.com/images/wrestling.jpg";

            var rep = new EventRepository(context);

            var existingEvent = await context.Events
                .SingleOrDefaultAsync(e => e.Id == EventsContextFactory.EventIdForUpdate);

            existingEvent.Title = title;
            existingEvent.Description = description;
            existingEvent.EventDateTime = eventDateTime;
            existingEvent.Location = location;
            existingEvent.Category = category;
            existingEvent.MaxParticipants = maxParticipants;
            existingEvent.ImageUrl = imageUrl;

            await rep.Update(existingEvent, CancellationToken.None);
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
