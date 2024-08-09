using Events.Application.Interfaces;
using Events.Domain;
using Microsoft.EntityFrameworkCore;
using Events.Persistence.EntityTypeConfiguration;

namespace Events.Persistence
{
    public class EventsDbContext : DbContext, IEventsDbContext
    {
        public DbSet<Event> Events {  get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Participation> Participations { get; set; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipationConfiguration());
        }
    }
} 
