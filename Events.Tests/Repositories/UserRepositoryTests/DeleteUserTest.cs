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
    public class DeleteUserTest : TestRepositoryBase
    {
        [Fact]
        public async Task DeleteUser()
        {
            var rep = new UserRepository(context);
            await rep.Delete(EventsContextFactory.UserBId, CancellationToken.None);
            await context.SaveChangesAsync();
            Assert.Null(context.Events.SingleOrDefault(e =>
                e.Id == EventsContextFactory.UserBId));
        }

        [Fact]
        public async Task DeleteUser_NotFound()
        {
            var rep = new UserRepository(context);
            await Assert.ThrowsAsync<NotFoundException>(async () => await rep.Delete(Guid.NewGuid(), CancellationToken.None));
        }
    }
}
