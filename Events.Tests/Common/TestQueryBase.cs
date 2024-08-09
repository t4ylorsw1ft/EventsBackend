using AutoMapper;
using Events.Application.Common.Mappings;
using Events.Application.Interfaces;
using Events.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Common
{
    public class TestQueryBase : IDisposable
    {
        protected readonly EventsDbContext context;
        protected readonly UnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public TestQueryBase()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IEventsDbContext).Assembly));
            });

            context = EventsContextFactory.Create();
            unitOfWork = new UnitOfWork(context);
            mapper = configurationProvider.CreateMapper();
        }



        public void Dispose()
        {
            EventsContextFactory.Destroy(context);
        }
    }
}