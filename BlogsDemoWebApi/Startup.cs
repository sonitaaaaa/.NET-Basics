using BlogsDEMO.Application;
using BlogsDEMO.Application.Interfaces;
using BlogsDemoWebApi.identityAuth;
using Core.Application;
using HealthChecks.UI.Client;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Seeds;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Net.Mime;
using System.Text;
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
            /*services.AddSwaggerExtension();
            services.AuthenticationExtensions(Config);*/
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Config.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;  // Cần phải xác thực Email
                options.SignIn.RequireConfirmedPhoneNumber = false;  // Cần phải xác thực SĐT
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789-._@+";
                options.Password.RequireDigit = true; // Phải chứa chữ số
                options.Password.RequiredLength = 6; // Độ dài tối thiểu 6
                options.Password.RequireLowercase = true; // Chữ thường
                options.Password.RequireNonAlphanumeric = true; // Ký tự đặc biệt
                options.Password.RequireUppercase = true; // In hoa
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Config["JWTSettings:Issuer"],
                    ValidAudience = Config["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWTSettings:Key"]))
                };
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2); // Thoi gian ton tai cua Token = ... Tieng(Hour)
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Blogs.WebApi",
                    Version = "v1",
                    Description = "Blog ASP.NET Core 5.0  Auth with JWT & Swagger"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter `Bearer`[space] and then your valid token in the text input below. \r\n\r\nExample: \"Bearer abcabcabc\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }
                        , new string[] {}
                    }
                });
            });
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