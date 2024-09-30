using FluentValidation;
using PetFamily.Application;
using PetFamily.Application.Volunteers;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IVolunteerRepository,VolunteerRepository>();
builder.Services.AddScoped<ICreateVolunteerService, CreateVolunteerService>();

builder.Services
    .AddApplication();

var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
