using Events.Application.Interfaces;
using Events.Application.Interfaces.IRepositories;
using Events.Persistence.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventsDbContext _dbContext;

        public UnitOfWork(EventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public IRepository<T> Repository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }

        public IEventRepository EventRepository()
        {
            return new EventRepository(_dbContext);
        }

        public IUserRepository UserRepository()
        {
            return new UserRepository(_dbContext);
        }

        public IParticipationRepository ParticipationRepository()
        {
            return new ParticipationRepository(_dbContext);
        }

    }
}
