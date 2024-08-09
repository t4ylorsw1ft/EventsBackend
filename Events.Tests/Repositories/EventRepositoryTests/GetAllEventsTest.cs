using Events.Application.Events.Queries.GetEventList;
using Events.Persistence.Repositories;
using Events.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Repositories.EventRepositoryTests
{
    public class GetAllEventsTest : TestRepositoryBase
    {
        [Fact]
        public async Task GetAllEvents()
        {
            var rep = new EventRepository(context);

            var result = await rep.GetAll();

            Assert.Equal(result, context.Events);
        }
    }
}
