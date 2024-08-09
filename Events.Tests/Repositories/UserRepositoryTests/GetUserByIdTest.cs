using Events.Application.Common.Exceptions;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.UserRepositoryTests
{
    public class GetUserByIdTest : TestRepositoryBase
    {
        [Fact]
        public async Task GetUserById_Success()
        {
            var rep = new UserRepository(context);
            var expectedUserId = EventsContextFactory.UserAId;

            var result = await rep.GetById(expectedUserId, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(expectedUserId, result.Id);
            Assert.Equal("Alice", result.Name);
            Assert.Equal("Johnson", result.Surname);
            Assert.Equal(new DateTime(1990, 4, 10), result.BirthDate);
            Assert.Equal("alice.johnson@example.com", result.Email);
            Assert.Equal("securepassword1", result.Password);
            Assert.True(result.IsAdmin);
            Assert.Empty(result.Participations); // Assuming no participations for simplicity
            Assert.Equal("refreshToken1", result.RefreshToken);
        }

        [Fact]
        public async Task GetUserById_Failure()
        {
            var rep = new UserRepository(context);
            await Assert.ThrowsAsync<NotFoundException>(async () => await rep.GetById(Guid.NewGuid(), CancellationToken.None));
        }
    }

}