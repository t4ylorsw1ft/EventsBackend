using Events.Application.Interfaces;
using Events.Persistence;
using Events.Persistence.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Common
{
    public class TestCommandBase
    {
        protected readonly EventsDbContext context;
        protected readonly UnitOfWork unitOfWork;
        protected readonly PasswordHasher passwordHasher;


        public TestCommandBase()
        {
            context = EventsContextFactory.Create();
            unitOfWork = new UnitOfWork(context);
            passwordHasher = new PasswordHasher();
        }


        public void Dispose()
        {
            EventsContextFactory.Destroy(context);
        }
    }
}
