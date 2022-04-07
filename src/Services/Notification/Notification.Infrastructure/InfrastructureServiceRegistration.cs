using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Contracts;
using Notification.Application.Models;
using Notification.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        //IServiceCollection is from using Microsoft.Extensions.DependencyInjection.
        //IConfiguration is from using Microsoft.Extensions.Configuration
        //UseSqlServer is from using Microsoft.EntityFrameworkCore
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //registering the Db connection using the configuration.

            services.AddScoped<INotificationRepository, NotificationRepository>();

            //to get the email settings from the appsetting.json
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            return services;
        }
    }
}
