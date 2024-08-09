using Events.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetByRefreshToken(string refreshToken, CancellationToken cancellationToken);
    }
}
