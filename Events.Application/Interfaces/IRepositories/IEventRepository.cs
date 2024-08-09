using Events.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Interfaces.IRepositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
