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

            app.UseApplicationSpec();

            app.Run();
        }
    }
}
