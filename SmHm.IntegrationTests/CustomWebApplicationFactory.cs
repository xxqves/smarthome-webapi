using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmHm.Core.Abstractions;
using SmHm.Core.Abstractions.Messaging;
using SmHm.IntegrationTests.FakeDependencies;
using SmHm.Persistence.PostgreSql;
using SmHm.WebApi;

namespace SmHm.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Production");

            builder.ConfigureServices(services =>
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.Scheme;
                    options.DefaultChallengeScheme = TestAuthHandler.Scheme;
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.Scheme, _ => { });

                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<SmartHomeDbContext>));
                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                var efCoreDescriptors = services
                    .Where(d => d.ServiceType.Namespace != null &&
                                d.ServiceType.Namespace.StartsWith("Microsoft.EntityFrameworkCore"))
                    .ToList();
                foreach (var d in efCoreDescriptors)
                {
                    services.Remove(d);
                }

                services.AddDbContext<SmartHomeDbContext>(options =>
                {
                    options.UseInMemoryDatabase("SmartHome_TestDb");
                });

                services.RemoveAll(typeof(IMessageBus));
                services.AddScoped<IMessageBus, FakeMessageBus>();

                services.RemoveAll(typeof(ICurrentUserService));
                services.AddScoped<ICurrentUserService, FakeCurrentUserService>();
            });
        }

    }
}
