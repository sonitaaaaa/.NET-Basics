using BlogsDEMO.Application;
using BlogsDEMO.Application.Interfaces;
using Core.Application;
using HealthChecks.UI.Client;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Seeds;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using WebApplication.Extensions;
using WebApplication.Services;

namespace WebApplication
{
    public class Startup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration config)
        {
            Config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddPersistenceInfrastructure(Config);
            services.AddSharedInfrastructure(Config);
            services.AddSwaggerExtension();
            services.AuthenticationExtensions(Config);
            services.AddCorsExtensions();
            services.AddSharedInfrastructure(Config);
            services.AddControllers().AddNewtonsoftJson();
            services.AddApiVersioningExtension();
            services.AddHealthCheckExtension(Config);
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            // Seeding Data
            services.AddScoped<SeedInitialData>();
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/health");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
                endpoints.MapHealthChecks("/hc-details",
                    new HealthCheckOptions
                    {
                        ResponseWriter = async (context, report) =>
                        {
                            var result = JsonSerializer.Serialize(
                                new
                                {
                                    status = report.Status.ToString(),
                                    monitors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                                });
                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            await context.Response.WriteAsync(result);
                        }
                    }
                );

                endpoints.MapControllers();
            });
        }
    }
}