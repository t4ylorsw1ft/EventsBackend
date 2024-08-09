using Events.Domain;
using Events.Persistence;
using Events.Persistence.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Common
{
    public class EventsContextFactory
    {
        private static PasswordHasher hasher = new PasswordHasher();
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();
        public static Guid UserCId = Guid.NewGuid();

        public static Guid ParticipationAId = Guid.NewGuid();
        public static Guid ParticipationBId = Guid.NewGuid();


        public static Guid EventIdForDelete = Guid.NewGuid();
        public static Guid EventIdForUpdate = Guid.NewGuid();

        public static EventsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<EventsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new EventsDbContext(options);
            context.Database.EnsureCreated();

            context.Events.AddRange(
                new Event
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    Title = "Rock Concert 1",
                    Description = "Join us for an electrifying night with top rock bands!",
                    EventDateTime = new DateTime(2024, 8, 15, 20, 0, 0),
                    Location = "Madison Square Garden, New York",
                    Category = "Entertainment",
                    MaxParticipants = 3,
                    Participations = new List<Participation>(), 
                    ImageUrl = "https://example.com/images/rock_concert.jpg"
                },
                new Event
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
                    Title = "Tech Conference 2024 - Session 1",
                    Description = "Explore the latest in technology and innovation.",
                    EventDateTime = new DateTime(2024, 9, 10, 9, 0, 0),
                    Location = "Silicon Valley, California",
                    Category = "Technology",
                    MaxParticipants = 3,
                    Participations = new List<Participation>(),
                    ImageUrl = "https://example.com/images/tech_conference.jpg"
                },
                new Event
                {
                    Id = EventIdForUpdate,
                    Title = "City Marathon - Charity Run",
                    Description = "Participate in our annual marathon for charity.",
                    EventDateTime = new DateTime(2024, 10, 5, 7, 30, 0),
                    Location = "Central Park, New York",
                    Category = "Sports",
                    MaxParticipants = 3,
                    Participations = new List<Participation>(),
                    ImageUrl = "https://example.com/images/marathon.jpg"
                },
                new Event
                {
                    Id = EventIdForDelete,
                    Title = "Rock Concert 2",
                    Description = "Another night of great rock music.",
                    EventDateTime = new DateTime(2024, 8, 15, 20, 0, 0),
                    Location = "Madison Square Garden, New York",
                    Category = "Entertainment",
                    MaxParticipants = 3,
                    Participations = new List<Participation>(),
                    ImageUrl = "https://example.com/images/rock_concert.jpg"
                }
            );

            context.Users.AddRange(
                new User
                {
                    Id = UserAId,
                    Name = "Alice",
                    Surname = "Johnson",
                    BirthDate = new DateTime(1990, 4, 10),
                    Email = "alice.johnson@example.com",
                    Password = "securepassword1",
                    IsAdmin = true,
                    Participations = new List<Participation>(),
                    RefreshToken = "refreshToken1"
                },
                new User
                {
                    Id = UserBId,
                    Name = "Bob",
                    Surname = "Smith",
                    BirthDate = new DateTime(1985, 12, 20),
                    Email = "bob.smith@example.com",
                    Password = "securepassword2",
                    IsAdmin = false,
                    Participations = new List<Participation>(),
                    RefreshToken = "refreshToken2"
                },
                new User
                {
                    Id = UserCId,
                    Name = "Bob",
                    Surname = "Smith",
                    BirthDate = new DateTime(1985, 12, 20),
                    Email = "bob2.smith@example.com",
                    Password = hasher.Generate("securepassword2"),
                    IsAdmin = false,
                    Participations = new List<Participation>(),
                    RefreshToken = "refreshToken2"
                }
            );
            context.Participations.AddRange(
                new Participation
                {
                    Id = ParticipationAId,
                    RegistrationDateTime = new DateTime(1990, 4, 10),
                    EventId = EventIdForUpdate,
                    UserId = UserBId
                },
                new Participation
                {
                    Id = ParticipationBId,
                    RegistrationDateTime = new DateTime(1990, 4, 10),
                    EventId = EventIdForUpdate,
                    UserId = UserCId
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(EventsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

    }
}
