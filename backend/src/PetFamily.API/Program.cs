using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using PetFamily.API.Extensions;
using PetFamily.Application;
using System.Text;
using Serilog;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Volunteers.Presentation;
using PetFamily.Species.Infrastructure;
using PetFamily.Species.Presentation;
using PetFamily.Accounts.Infrastructure.Seeding;
using Microsoft.AspNetCore.Authorization;
using PetFamily.Framework.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Presentation;
using PetFamily.Volunteers.Infrastructure;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Infrastructure;
using PetFamily.Discussion.Presentation;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});

builder.Services
    .AddVolunteersInfrastructure(builder.Configuration)
    .AddVolunteersPresentation()
    .AddVolunteersApplication()
    .AddSpeciesApplication()
    .AddSpeciesInfrastructure(builder.Configuration)
    .AddSpeciesPresentation()
    .AddAuthorizationInfrastructure(builder.Configuration)
    .AddAccountApplication()
    .AddAccountsPresentation()
    .AddVolunteerRequestApplication()
    .AddVolunteerRequestInfrastructure(builder.Configuration)
    .AddDiscussionPresentation()
    .AddVolunteerRequestInfrastructure(builder.Configuration)
    .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
    .AddSingleton<IAuthorizationHandler, PermissionsRequirementHandler>();



var app = builder.Build();

var accountsSeeder = app.Services.GetRequiredService<AdminAccountsSeeder>();

// убрать тут cancellation 
await accountsSeeder.Seed(CancellationToken.None);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //await app.AddMigrations();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
//app.UseSerilogRequestLogging();

app.Run();