using Events.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Persistence.EntityTypeConfiguration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Participations)
              .WithOne(p => p.Event)
              .HasForeignKey(p => p.EventId);

            builder.Property(e => e.Title).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(1000);
            builder.Property(e => e.Location).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(e => e.Category).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.ImageUrl).HasColumnType("varchar").HasMaxLength(250);



        }
    }
}
