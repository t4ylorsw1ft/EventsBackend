using Events.Persistence.Repositories;
using Events.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.UserRepositoryTests
{
    public class UpdateUserTest : TestRepositoryBase
    {
        [Fact]
        public async Task UpdateUser()
        {
            string name = "Alice";
            string surname = "Smith";
            DateTime birthDate = new DateTime(1991, 5, 15);
            string email = "alice.smith@example.com";
            string password = "newsecurepassword";
            bool isAdmin = false;
            string refreshToken = "newRefreshToken";

            var rep = new UserRepository(context);

            var existingUser = await context.Users
                .SingleOrDefaultAsync(u => u.Id == EventsContextFactory.UserAId);

            existingUser.Name = name;
            existingUser.Surname = surname;
            existingUser.BirthDate = birthDate;
            existingUser.Email = email;
            existingUser.Password = password;
            existingUser.IsAdmin = isAdmin;
            existingUser.RefreshToken = refreshToken;

            await rep.Update(existingUser, CancellationToken.None);
            await context.SaveChangesAsync();

            Assert.NotNull(await context.Users.SingleOrDefaultAsync(u =>
                u.Id == EventsContextFactory.UserAId &&
                u.Name == name &&
                u.Surname == surname &&
                u.BirthDate == birthDate &&
                u.Email == email &&
                u.Password == password &&
                u.IsAdmin == isAdmin &&
                u.RefreshToken == refreshToken));
        }

    }
}
