using Events.Application.Interfaces;
using Events.Application.Interfaces.IRepositories;
using Events.Domain;
using Events.Persistence.Auth;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence ( this IServiceCollection services, 
            IConfiguration configuration )
        { 
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<EventsDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            services.AddScoped<IEventsDbContext>(provider => 
                provider.GetService<EventsDbContext>());
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<Event>, GenericRepository<Event>>();
            services.AddScoped<IRepository<User>, GenericRepository<User>>();
            services.AddScoped<IRepository<Participation>, GenericRepository<Participation>>();
            return services;
        }

    }
}
