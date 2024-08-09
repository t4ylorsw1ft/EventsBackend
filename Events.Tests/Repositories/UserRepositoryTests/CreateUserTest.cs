using Events.Domain;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Events.Tests.Repositories.UserRepositoryTests
{
    public class CreateUserTest : TestRepositoryBase
    {
        private Guid id = Guid.NewGuid();

        [Fact]
        public async Task CreateUser()
        {
            Guid Id = Guid.NewGuid();
            string Name = "Alice";
            string Surname = "Johnson";
            DateTime BirthDate = new DateTime(1990, 4, 10);
            string Email = "alice.johnson@example.com";
            string Password = "securepassword1";
            bool IsAdmin = true;
            List<Participation> Participations = new List<Participation>();
            string RefreshToken = "refreshToken1";

            var rep = new UserRepository(context);
            var user = new User
            {
                Id = Id,
                Name = Name,
                Surname = Surname,
                BirthDate = BirthDate,
                Email = Email,
                Password = Password,
                IsAdmin = IsAdmin,
                Participations = Participations,
                RefreshToken = RefreshToken
            };

            await rep.Create(user, CancellationToken.None);
            await context.SaveChangesAsync();

            Assert.NotNull(await context.Users.SingleOrDefaultAsync(u =>
                u.Id == Id &&
                u.Name == Name &&
                u.Surname == Surname &&
                u.BirthDate == BirthDate &&
                u.Email == Email &&
                u.Password == Password &&
                u.IsAdmin == IsAdmin &&
                u.RefreshToken == RefreshToken));
        }
    }
}
