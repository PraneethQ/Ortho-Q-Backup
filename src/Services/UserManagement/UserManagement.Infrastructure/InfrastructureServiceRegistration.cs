using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Infrastructure;
using UserManagement.Application.Contracts.Persistance;
using UserManagement.Application.Models;
using UserManagement.Infrastructure.Mail;
using UserManagement.Infrastructure.Persistance;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        //IServiceCollection is from using Microsoft.Extensions.DependencyInjection.
        //IConfiguration is from using Microsoft.Extensions.Configuration
        //UseSqlServer is from using Microsoft.EntityFrameworkCore
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //registering the Db connection using the configuration.

            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserManagementRepository, UserManagementRepository>();


            //to get the email settings from the appsetting.json
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
