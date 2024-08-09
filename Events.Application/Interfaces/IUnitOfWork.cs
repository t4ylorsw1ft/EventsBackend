using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Application.Interfaces.IRepositories;

namespace Events.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        IRepository<T> Repository<T>() where T : class;

        IEventRepository EventRepository();
        IUserRepository UserRepository();
        IParticipationRepository ParticipationRepository();

        
    }
}
