using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Appointment.Application;
using Appointment.Application.Behaviours;

namespace Appointment.Application
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

            /*
              AddValidatorsFromAssembly is not a directly available in IServiceCollection. 
              we need to install a package :-
              install-package FluentValidation.DependencyInjectionExtensions
              which will register the all validators [IValidator, AbstractValidator etc..]
              with startup.cs
            */
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // AddMediatR is from using MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Register the pipe line behaviors 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
