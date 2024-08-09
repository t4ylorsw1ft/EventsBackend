using Events.Domain;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.EventRepositoryTests
{
    public class CreateEventTest : TestRepositoryBase
    {
        private Guid id = Guid.NewGuid();

        [Fact]
        public async Task CreateEvent()
        {
            string title = "Pop Concert";
            string description = "Join us for an electrifying night with top pop bands!";
            DateTime eventDateTime = new DateTime(2024, 9, 15, 20, 0, 0);
            string location = "Madison Square Garden, New York";
            string category = "Entertainment";
            int maxParticipants = 3;
            string imageUrl = "https://example.com/images/rock_concert.jpg";

            var rep = new EventRepository(context);
            var eventt = new Event
            {
                Id = id,
                Title = title,
                Description = description,
                EventDateTime = eventDateTime,
                Location = location,
                Category = category,
                MaxParticipants = maxParticipants,
                Participations = new List<Participation>(),
                ImageUrl = imageUrl
            };

            await rep.Create(eventt, CancellationToken.None);
            await context.SaveChangesAsync();

            Assert.NotNull(await context.Events.SingleOrDefaultAsync(e => 
                e.Id == id && 
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
