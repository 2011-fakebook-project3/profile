using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Fakebook.Profile.DataAccess;
using Fakebook.Profile.DataAccess.Services.Interfaces;
using Fakebook.Profile.Domain;
using Fakebook.Profile.RestApi.Controllers;
using Fakebook.Profile.UnitTests.ApiTests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.IntegrationTests
{
    public class ProfileControllerIntegrationTests
    {
        [Fact]
        public async Task AddFollowerAsync()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, "john werner"),
                new Claim(ClaimTypes.NameIdentifier, "john.werner@revature.net"),
                new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var repo = new ProfileRepository(context);
            Mock<IStorageService> mockedStorageService = new();
            ProfileController controller = new(
                repo,
                mockedStorageService.Object,
                new NullLogger<ProfileController>()
            );
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            DomainProfile test = new DomainProfile("tdunbar@google.com", "Trevor", "Dunbar");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            await repo.CreateProfileAsync(test);

            var result = await controller.Follow(test.Email);
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public async Task UnfollowAsync()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "john werner"),
                new Claim(ClaimTypes.NameIdentifier, "john.werner@revature.net"),
                new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var repo = new ProfileRepository(context);
            Mock<IStorageService> mockedStorageService = new();
            ProfileController controller = new(
                repo,
                mockedStorageService.Object,
                new NullLogger<ProfileController>()
            );
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            DomainProfile test = new DomainProfile("tdunbar@google.com", "Trevor", "Dunbar");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            await repo.CreateProfileAsync(test);

            DomainProfile userDomain = await repo.GetProfileAsync("john.werner@revature.net");

            // Make userDomain follow test user

            userDomain.AddFollow(test.Email);
            test.AddFollower(userDomain.Email);

            // Update database
            await repo.UpdateProfileAsync(userDomain.Email, userDomain);
            await repo.UpdateProfileAsync(test.Email, test);

            // Unfollow test user and assert
            var result = await controller.Unfollow(test.Email);
            userDomain = await repo.GetProfileAsync(userDomain.Email);
            test = await repo.GetProfileAsync(test.Email);

            Assert.IsAssignableFrom<OkResult>(result);
            Assert.DoesNotContain(test.Email, userDomain.FollowingEmails);
            Assert.DoesNotContain(userDomain.Email, test.FollowerEmails);
        }
    }
}
