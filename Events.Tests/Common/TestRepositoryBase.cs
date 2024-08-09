using Events.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Common
{
    public class TestRepositoryBase : IDisposable
    {
        protected readonly EventsDbContext context;

        public TestRepositoryBase()
        {
            context = EventsContextFactory.Create();
        }

        public void Dispose()
        {
            EventsContextFactory.Destroy(context);
        }
    }
}
