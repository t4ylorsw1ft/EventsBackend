using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.UserRepositoryTests
{
    public class GetAllUsersTest : TestRepositoryBase
    {
        [Fact]
        public async Task GetAllUsers()
        {
            var rep = new UserRepository(context);

            var result = await rep.GetAll();

            Assert.Equal(result, context.Users);
        }
    }
}
