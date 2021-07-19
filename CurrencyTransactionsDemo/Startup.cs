using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using CurrencyTransactionsDemo.Core.Utilities.IoC;
using CurrencyTransactionsDemo.DependencyResolvers;
using CurrencyTransactionsDemo.Core.Extensions;
using CurrencyTransactionsDemo.LoggingOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;

namespace CurrencyTransactionsDemo
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

            services.AddControllers();
            services.AddHangfire(
                config =>
                {
                    var options = new SqlServerStorageOptions
                    {
                        PrepareSchemaIfNecessary = true,
                        QueuePollInterval = TimeSpan.FromMinutes(5),
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    };
                    config.UseSqlServerStorage(@"Server=(localdb)\mssqllocaldb;Database=Log;Trusted_Connection=true");
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CurrencyTransactionsDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyTransactionsDemo v1"));
            }
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseHangfireDashboard("/BackgroundJobs", new DashboardOptions
            {
                DashboardTitle = "Hatipoglu Hangfire Dashboard", 
                AppPath = "/weatherforecast"
            });
            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                
                SchedulePollingInterval = TimeSpan.FromSeconds(5)
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdatesHangfire.UpdateCurrency();
            UpdatesHangfire.UpdateCurrencySecond();
        }
    }
}
