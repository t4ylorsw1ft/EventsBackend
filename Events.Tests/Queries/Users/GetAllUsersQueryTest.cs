using AutoMapper;
using Events.Application.Events.Queries.GetEventList;
using Events.Application.Users.Queries.GetUserList;
using Events.Persistence;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Queries.Users
{
    public class GetAllUsersQueryTest : TestQueryBase
    {
        [Fact]
        public async Task GetUserList()
        {
            var handler = new GetUserListQueryHandler(unitOfWork, mapper);

            var result = await handler.Handle(
                new GetUserListQuery(),
                CancellationToken.None);

            Assert.IsType<UserListVm>(result);
            Assert.True(result.Users.Count == 3);
        }
    }
}
