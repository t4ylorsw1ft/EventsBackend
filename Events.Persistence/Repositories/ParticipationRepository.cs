using Events.Application.Common.Exceptions;
using Events.Application.Interfaces.IRepositories;
using Events.Domain;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Persistence.Repositories
{
    public class ParticipationRepository : GenericRepository<Participation>, IParticipationRepository
    {
        private readonly EventsDbContext _dbContext;
        private readonly DbSet<Participation> _dbSet;
        public ParticipationRepository(EventsDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Participation>();
        }

        public async Task Create(Participation entity, CancellationToken cancellationToken)
        {

            if (await _dbSet.AnyAsync(part => part.UserId == entity.UserId && part.EventId == entity.EventId))
            {
                throw new Exception("Such record already exists");
            }

            var ev = await _dbContext.Events.Include(u => u.Participations).FirstOrDefaultAsync(eventt => eventt.Id == entity.EventId);

            if (ev.Participations.Count >= ev.MaxParticipants)
            {
                throw new NoPlacesLeftException(ev.Id, ev.MaxParticipants);
            }

            if (ev.EventDateTime < DateTime.Now)
            {
                throw new Exception("this event has already taken place");
            }

            await _dbSet.AddAsync(entity);
        }
    }
}
