using System;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
        await dbContext.Database.MigrateAsync();

        var logger = app.Services.GetRequiredService<ILoggerProvider>().CreateLogger("DataExtensions.MigrateDbAsync");
        logger.LogInformation(1, "Database is ready");
    }
}
