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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(u => u.Participations)
              .WithOne(p => p.User)
              .HasForeignKey(p => p.UserId);

            builder.Property(u => u.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(u => u.Surname).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(u => u.Email).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(u => u.Password).HasColumnType("varchar").HasMaxLength(72);
            builder.Property(u => u.RefreshToken).HasColumnType("varchar").HasMaxLength(200);

            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.RefreshToken).IsUnique();
        }
    }
}
