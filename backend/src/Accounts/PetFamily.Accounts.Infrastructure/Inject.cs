using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Application.Authorization;
using PetFamily.Infrastructure.Authentication.Options;
using PetFamily.Infrastructure.Authentication.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Authentication
{
    public static class Inject
    {
        public static IServiceCollection AddAuthorizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthorizationDbContext>();

            services.AddOptions<JwtOptions>();

            services.Configure<JwtOptions>(
                configuration.GetSection(JwtOptions.JWT));

            services.AddTransient<ITokenProvider, JwtTokenProvider>();


            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthorizationDbContext>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
                     ?? throw new ArgumentException("Missing Jwt configuration");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true, 
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddAuthorization();
            
            // services.AddAuthorization(options =>
            // {
            //     options.DefaultPolicy = new AuthorizationPolicyBuilder()
            //     .RequireClaim("Role", "User")
            //     .RequireAuthenticatedUser()
            //     .Build();
            //
            //     options.AddPolicy("GetAllPetsRequirement", policy =>
            //     {
            //         policy.AddRequirements(new PermissionRequirement("Pet"));
            //     });
            // });

            services.AddSingleton<IAuthorizationHandler, PermissionsRequirementHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            return services;
        }

    }
}
