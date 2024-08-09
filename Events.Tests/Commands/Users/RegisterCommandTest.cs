using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Interfaces;
using Events.Application.Users.Commands.Register;
using Events.Domain;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Commands.Users
{
    public class RegisterCommandTest : TestCommandBase
    {
        [Fact]
        public async Task Register()
        {
            var handler = new RegisterCommandHandler(unitOfWork, passwordHasher);

            string Name = "Alice";
            string Surname = "Johnson";
            DateTime BirthDate = new DateTime(1990, 4, 10);
            string Email = "alice.johnson@example.com";
            string Password = "securepassword1";
            bool IsAdmin = true;
            List<Participation> Participations = new List<Participation>();
            string RefreshToken = "refreshToken1";

            var rep = new UserRepository(context);
            var userId = await handler.Handle(
                new RegisterCommand
                {
                    Name = Name,
                    Surname = Surname,
                    BirthDate = BirthDate,
                    Email = Email,
                    Password = Password
                }, 
                CancellationToken.None);

            Assert.NotNull(await context.Users.SingleOrDefaultAsync(u =>
                u.Id == userId &&
                u.Name == Name &&
                u.Surname == Surname &&
                u.BirthDate == BirthDate &&
                u.Email == Email));
        }

        public async Task Register_EmailExists()
        {
            var handler = new RegisterCommandHandler(unitOfWork, passwordHasher);

            string Name = "Alice";
            string Surname = "Johnson";
            DateTime BirthDate = new DateTime(1990, 4, 10);
            string Email = "alice.johnson@example.com";
            string Password = "securepassword1";
            bool IsAdmin = true;
            List<Participation> Participations = new List<Participation>();
            string RefreshToken = "refreshToken1";

            var rep = new UserRepository(context);

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(
                new RegisterCommand
                {
                    Name = Name,
                    Surname = Surname,
                    BirthDate = BirthDate,
                    Email = "bob.smith@example.com",
                    Password = Password
                },
                CancellationToken.None));
        }
    }
}
