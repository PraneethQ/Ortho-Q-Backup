using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Notification.API.EventBusConsumer;
using Notification.Application;
using Notification.Infrastructure;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Exporter;

namespace Notification.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);

            // MassTransit-RabbitMQ Configuration
            /* 
             As ordering.api is subscriber and consume the the event so, we have to add a consumer
             code in the ordering.api startup.cs.
             
             we have to create consumer class. which tell the consumer [ordering.api] that 
             the event is published and it is in match with consumer class.
             */
            services.AddMassTransit(config => {
                config.AddConsumer<NotificationEventConsumer>();
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);

                    //this is for subscribing the microservice to the queue.
                    // we have to provide the queue name->"subscriber-notification-mail"
                    cfg.ReceiveEndpoint(EventBusConstants.NotificationMail, c =>
                    {
                        c.ConfigureConsumer<NotificationEventConsumer>(ctx);
                    });
                });
            });

            services.AddMassTransitHostedService();

            //General configuration
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<NotificationEventConsumer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification.API", Version = "v1" });
            });
            services.AddOpenTelemetryTracing(options =>
            {
                var tracingExporter = "jaeger";
                options
                    .SetSampler(new AlwaysOnSampler())
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation();

                switch (tracingExporter)
                {
                    case "jaeger":
                        options.AddJaegerExporter();

                        services.Configure<JaegerExporterOptions>(Configuration.GetSection("Jaeger"));
                        break;

                    case "zipkin":
                        options.AddZipkinExporter();

                        services.Configure<ZipkinExporterOptions>(Configuration.GetSection("Zipkin"));
                        break;

                    case "otlp":
                        options.AddOtlpExporter(otlpOptions =>
                        {
                            otlpOptions.Endpoint = new Uri(Configuration.GetValue<string>("Otlp:Endpoint"));
                        });
                        break;

                    default:
                        options.AddConsoleExporter();

                        break;
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification.API v1"));
            }

            //configuration to serve the static files from .net core web api. [ex : Images in our case]
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
