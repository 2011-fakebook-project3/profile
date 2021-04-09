using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.RestApi;
using Fakebook.Profile.RestApi.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Fakebook.Profile.UnitTests.ApiTests
{
    public class SqliteWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly SqliteConnection _connection;

        public SqliteWebApplicationFactory()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IPolicyEvaluator>();
                services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();

                Mock<IConfiguration> mockedStorageConfiguration = new();
                Mock<IStorageService> mockedStorageService = new();
                mockedStorageService
                    .Setup(x => x.UploadFileContentAsync(It.IsAny<Stream>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                    .Returns(Task.FromResult(new Uri("https://www.fake.com")));
                services.RemoveAll<IStorageService>();
                services.AddSingleton<IStorageService>(mockedStorageService.Object);
                services.AddSingleton<ILogger>(new NullLogger<ProfileController>());

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ProfileDbContext>));
                services.Remove(descriptor);

                services.AddDbContext<ProfileDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                services.AddControllers(opts =>
                {
                    opts.Filters.Add<AllowAnonymousFilter>();
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ProfileDbContext>();

                db.Database.EnsureCreated();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Close();
        }
    }
}
