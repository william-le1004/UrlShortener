using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;

namespace UrlShortener.Extension;

public static class MigrationExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UrlShortenerContext>();

        dbContext.Database.Migrate();
    }
}
