using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Contracts.Persistance;
using Order.Application.Models;
using Order.Infrastructure.Persistance;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        //IServiceCollection is from using Microsoft.Extensions.DependencyInjection.
        //IConfiguration is from using Microsoft.Extensions.Configuration
        //UseSqlServer is from using Microsoft.EntityFrameworkCore
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //registering the Db connection using the configuration.

            services.AddScoped<IOrderContext, OrderContext>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
