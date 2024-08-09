using Events.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Interfaces
{
    public interface IEventsDbContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Participation> Participations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
