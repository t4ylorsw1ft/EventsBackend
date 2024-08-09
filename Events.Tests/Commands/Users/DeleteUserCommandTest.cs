using Events.Application.Common.Exceptions;
using Events.Application.Events.Commands.DeleteEvent;
using Events.Application.Users.Commands.DeleteUser;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Commands.Users
{
    public class DeleteUserCommandTest : TestCommandBase
    {
        [Fact]
        public async Task DeleteUser()
        {
            var handler = new DeleteUserCommandHandler(unitOfWork, passwordHasher);

            await handler.Handle(
                new DeleteUserCommand
                {
                    Id = EventsContextFactory.UserCId,
                    Password = "securepassword2"
                },
                CancellationToken.None);
            Assert.Null(context.Users.SingleOrDefault(user =>
                user.Id == EventsContextFactory.UserCId));
        }

        [Fact]
        public async Task DeleteUser_NotFound()
        {
            var handler = new DeleteUserCommandHandler(unitOfWork, passwordHasher);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new DeleteUserCommand
                {
                    Id = Guid.NewGuid(),
                    Password = "securepassword1"
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteUser_WrongPassword()
        {
            var handler = new DeleteUserCommandHandler(unitOfWork, passwordHasher);
            await Assert.ThrowsAsync<Exception>(async () =>
            await handler.Handle(
                new DeleteUserCommand
                {
                    Id = EventsContextFactory.UserCId,
                    Password = "securepassw"
                },
                CancellationToken.None));
        }
    }
}
