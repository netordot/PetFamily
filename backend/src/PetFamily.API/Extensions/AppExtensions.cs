﻿using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastructure;

namespace PetFamily.API.Extensions;

public static class AppExtensions
{
    //public static async Task AddMigrations(this WebApplication app)
    //{
    //    await using var scope = app.Services.CreateAsyncScope();
    //    var dbContext =  scope.ServiceProvider.GetRequiredService<WriteDbContext>();

    //    await dbContext.Database.MigrateAsync();   
    //}
}