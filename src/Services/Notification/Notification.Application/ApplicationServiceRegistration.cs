using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application
{
    public static class ApplicationServiceRegistration
    {
        //IServiceCollection is from using Microsoft.Extensions.DependencyInjection.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Assembly uses the 'System.Reflection'

            /*
             AddAutoMapper is not a directly available in IServiceCollection. we need to install
             a package : install-package Automapper.Extensions.Microsoft.DependencyInjection
             which will register the automapper with startup.cs
             */
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // AddMediatR is from using MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Register the pipe line behaviors 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
