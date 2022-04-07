using Member.Application.Contracts.Persistance;
using Member.Infrastructure.Persistance;
using Member.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        //IServiceCollection is from using Microsoft.Extensions.DependencyInjection.
        //IConfiguration is from using Microsoft.Extensions.Configuration
        //UseSqlServer is from using Microsoft.EntityFrameworkCore
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //registering the Db connection using the configuration.

            services.AddScoped<IMemberContext, MemberContext>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            return services;
        }
    }
}
