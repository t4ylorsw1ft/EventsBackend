
using Events.Application.Common.Exceptions;
using Events.Application.Interfaces.IRepositories;
using Events.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly EventsDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(EventsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public async Task Create(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Update(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            _dbSet.Remove(entity);
        }

        public async Task<IQueryable<T>> GetAll()
        {
            var entityList = _dbSet;

            return entityList;
        }

        public async Task<T> GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            return entity;
        }
    }
}
