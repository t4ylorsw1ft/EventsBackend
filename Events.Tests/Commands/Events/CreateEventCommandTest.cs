using Events.Application.Events.Commands.CreateEvent;
using Events.Domain;
using Events.Tests.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Commands.Events
{
    public class CreateEventCommandTest : TestCommandBase
    {
        [Fact]
        public async Task CreateEventCommandHandler_Success() 
        {
            var handler = new CreateEventCommandHandler(unitOfWork);

            string title = "Pop Concert";
            string description = "Join us for an electrifying night with top pop bands!";
            DateTime eventDateTime = new DateTime(2024, 9, 15, 20, 0, 0);
            string location = "Madison Square Garden, New York";
            string category = "Entertainment";
            int maxParticipants = 3;
            string imageUrl = "https://example.com/images/rock_concert.jpg";

            var eventId = await handler.Handle(
                new CreateEventCommand
                {
                    Title = title,
                    Description = description,
                    EventDateTime = eventDateTime,
                    Location = location,
                    Category = category,
                    MaxParticipants = maxParticipants,
                    ImageUrl = imageUrl
                }, 
                CancellationToken.None);

            Assert.NotNull(await context.Events.SingleOrDefaultAsync(e =>
               e.Id == eventId &&
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
