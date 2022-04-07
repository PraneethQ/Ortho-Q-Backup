using Appointment.Application.Contracts.Persistance;
using Appointment.Infrastructure.Persistance;
using Appointment.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        //IServiceCollection is from using Microsoft.Extensions.DependencyInjection.
        //IConfiguration is from using Microsoft.Extensions.Configuration
        //UseSqlServer is from using Microsoft.EntityFrameworkCore
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //registering the Db connection using the configuration.

            services.AddScoped<IAppointmentContext, AppointmentContext>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IAppointmenttRepository, AppointmentRepository>();


         

            return services;
        }
    }
}
