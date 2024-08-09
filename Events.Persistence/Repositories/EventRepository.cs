using Events.Application.Common.Exceptions;
using Events.Application.Interfaces;
using Events.Application.Interfaces.IRepositories;
using Events.Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Events.Persistence.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly EventsDbContext _dbContext;
        private readonly DbSet<Event> _dbSet;
        public EventRepository(EventsDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Event>();
        }

        public async Task<Event> GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.Include(e => e.Participations).ThenInclude(p => p.User).FirstOrDefaultAsync(eventt => eventt.Id == id);
            if (entity == null)
            {
                throw new NotFoundException(typeof(Event).Name, id);
            }

            return entity;
        }
    }
}
