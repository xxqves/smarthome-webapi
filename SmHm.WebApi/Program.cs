using Microsoft.EntityFrameworkCore;
using SmHm.Persistence.PostgreSql;
using SmHm.WebApi.Configuration;

namespace SmHm.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddConfiguration(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                dbContext.Database.Migrate();
            }

            app.UseApplicationSpec();

            app.Run();
        }
    }
}
