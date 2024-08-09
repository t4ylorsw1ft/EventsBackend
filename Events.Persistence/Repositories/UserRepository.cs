using Events.Application.Common.Exceptions;
using Events.Application.Interfaces.IRepositories;
using Events.Domain;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly EventsDbContext _dbContext;
        private readonly DbSet<User> _dbSet;
        public UserRepository(EventsDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<User>();
        }

        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _dbSet.FirstOrDefaultAsync(user =>
            user.Email == email, cancellationToken);

            if (user == null)
            {
                throw new LoginFailException();
            }

            return user;
        }

        public async Task<User> GetByRefreshToken(string email, CancellationToken cancellationToken)
        {
            var user = await _dbSet.FirstOrDefaultAsync(user =>
            user.RefreshToken == email, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User));
            }

            return user;
        }

        public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.Include(u => u.Participations).ThenInclude(p => p.Event).FirstOrDefaultAsync(user => user.Id == id);

            if (entity == null)
            {
                throw new NotFoundException(typeof(User).Name, id);
            }

            return entity;
        }


    }
}
