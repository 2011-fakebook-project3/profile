using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fakebook.Profile.Domain;
using Fakebook.Profile.DataAccess;

namespace Fakebook.Profile.UnitTests.APITests
{
    public class ProfileIntegrationTests
    {
        [Fact]
        public async Task ProfileRepoAddProfile()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            DomainProfile test = new DomainProfile("tdunbar@google.com", "Trevor", "Dunbar");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            var repo = new ProfileRepository(context);
            await repo.CreateProfileAsync(test);

            var compare = context.EntityProfiles.Local.Single(x => x.Id == 3); //ID is 3 because there's already 2 users in seed data
            Assert.Equal(test.Email, compare.Email);
            Assert.Equal(test.FirstName, compare.FirstName);
            Assert.Equal(test.LastName, compare.LastName);
        }

        [Fact]
        public async Task ProfileRepoGetProfile()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            var repo = new ProfileRepository(context);
            var test = await repo.GetProfileAsync("john.werner@revature.net");

            var compare = context.EntityProfiles.Local.Single(x => x.Id == 1); //ID is 3 because there's already 2 users in seed data
            Assert.Equal(test.Email, compare.Email);
            Assert.Equal(test.FirstName, compare.FirstName);
            Assert.Equal(test.LastName, compare.LastName);
        }

        [Fact]
        public async Task ProfileRepoTestFollowers()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            DomainProfile test = new DomainProfile("tdunbar@google.com", "Trevor", "Dunbar");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            DomainProfile testFriend = new DomainProfile("friend@google.com", "Friendly", "Person");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            var repo = new ProfileRepository(context);
            await repo.CreateProfileAsync(test); // ID 3
            await repo.CreateProfileAsync(testFriend); // ID 4

            test.FollowingEmails.Add("friend@google.com");
            testFriend.FollowerEmails.Add("tdunbar@google.com");

            testFriend.FollowingEmails.Add("tdunbar@google.com");
            test.FollowerEmails.Add("friend@google.com"); // 2 accounts that follow each other

            await repo.UpdateProfileAsync(test.Email, test);
            await repo.UpdateProfileAsync(testFriend.Email, testFriend);

            var compareTest = context.EntityProfiles.Local.Single(x => x.Id == 3);
            var compareTestFriend = context.EntityProfiles.Local.Single(x => x.Id == 4);

            Assert.NotEmpty(compareTest.Followers);
            Assert.NotEmpty(compareTest.Following);
            Assert.NotEmpty(compareTestFriend.Followers);
            Assert.NotEmpty(compareTestFriend.Following);
        }

        [Fact]
        public async Task ProfileRepoTestGettingFollowers()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            DomainProfile test = new DomainProfile("tdunbar@google.com", "Trevor", "Dunbar");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            DomainProfile testFriend = new DomainProfile("friend@google.com", "Friendly", "Person");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            var repo = new ProfileRepository(context);
            await repo.CreateProfileAsync(test); // ID 3
            await repo.CreateProfileAsync(testFriend); // ID 4

            test.FollowingEmails.Add("friend@google.com");
            testFriend.FollowerEmails.Add("tdunbar@google.com");

            testFriend.FollowingEmails.Add("tdunbar@google.com");
            test.FollowerEmails.Add("friend@google.com"); // 2 accounts that follow each other

            await repo.UpdateProfileAsync(test.Email, test);
            await repo.UpdateProfileAsync(testFriend.Email, testFriend);

            var compareTest = await repo.GetProfileAsync(test.Email);
            var compareTestFriend = await repo.GetProfileAsync(testFriend.Email);

            Assert.Contains("friend@google.com", compareTest.FollowerEmails);
            Assert.Contains("friend@google.com", compareTest.FollowingEmails);
            Assert.Contains("tdunbar@google.com", compareTestFriend.FollowerEmails);
            Assert.Contains("tdunbar@google.com", compareTestFriend.FollowingEmails);
        }
    }
}
