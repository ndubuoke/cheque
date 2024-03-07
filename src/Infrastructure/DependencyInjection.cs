using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Infrastructure.Persistence;
using ChequeMicroservice.Infrastructure.Services;
using RestSharp;
using Serilog.Events;
using Serilog;
using System.Text;

namespace ChequeMicroservice.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ChequeMicroserviceDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateLifetime = true
                        };
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.Configuration = new OpenIdConnectConfiguration();
                    });
            services.AddAuthorization();
            services.AddTransient<IAPIClientService, APIClientService>();
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<INotificationService, NotificationService>();
            return services;
        }


    }
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJwtBearerContractentication(this IServiceCollection services)
        {
            //var builder = services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //});

            //builder.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = true,
            //        ValidateIssuer = true,
            //        ValidateLifetime = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //            Encoding.UTF8.GetBytes(TokenConstants.key)),
            //        ValidIssuer = TokenConstants.Issuer,
            //        ValidAudience = TokenConstants.Audience
            //    };
            //});

            return services;
        }

        public static IServiceCollection AddRolesAndPolicyApiAuthorization(this IServiceCollection services)
        {
            return services;
        }
    }
}

