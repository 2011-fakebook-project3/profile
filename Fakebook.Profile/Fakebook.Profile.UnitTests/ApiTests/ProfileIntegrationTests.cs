using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fakebook.Profile.Domain;
using Fakebook.Profile.DataAccess;

namespace Fakebook.Profile.UnitTests.ApiTests
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
            int lastId = context.EntityProfiles.Count();

            var compare = context.EntityProfiles.Local.Single(x => x.Id == lastId);
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

            var compare = context.EntityProfiles.Local.Single(x => x.Id == 1);
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
            int testId = context.EntityProfiles.Count();
            await repo.CreateProfileAsync(testFriend); // ID 4
            int testFriendId = context.EntityProfiles.Count();

            test.FollowingEmails.Add("friend@google.com");
            testFriend.FollowerEmails.Add("tdunbar@google.com");

            testFriend.FollowingEmails.Add("tdunbar@google.com");
            test.FollowerEmails.Add("friend@google.com"); // 2 accounts that follow each other

            await repo.UpdateProfileAsync(test.Email, test);
            await repo.UpdateProfileAsync(testFriend.Email, testFriend);

            var compareTest = context.EntityProfiles.Local.Single(x => x.Id == testId);
            var compareTestFriend = context.EntityProfiles.Local.Single(x => x.Id == testFriendId);

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

        [Fact]
        public async Task TestSearchByFirstNameOnly()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            DomainProfile test = new DomainProfile("tdunbar@google.com", "Trevor", "Dunbar");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            var repo = new ProfileRepository(context);
            await repo.CreateProfileAsync(test);

            var search = await repo.GetProfilesByNameAsync("Trevor");

            var user = search.First();

            Assert.Equal("tdunbar@google.com", user.Email);
            Assert.Equal("Trevor", user.FirstName);
            Assert.Equal("Dunbar", user.LastName);
        }

        [Fact]
        public async Task TestSearchByLastNameOnly()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            DomainProfile test = new DomainProfile("tdunbar@google.com", "Trevor", "Dunbar");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            var repo = new ProfileRepository(context);
            await repo.CreateProfileAsync(test);

            var search = await repo.GetProfilesByNameAsync(lastName: "Dunbar");

            var user = search.First();

            Assert.Equal("tdunbar@google.com", user.Email);
            Assert.Equal("Trevor", user.FirstName);
            Assert.Equal("Dunbar", user.LastName);
        }

        [Fact]
        public async Task TestSearchByFirstNameAndLastName()
        {
            using var contextFactory = new ContextFactory();
            using var context = contextFactory.CreateContext();

            DomainProfile test = new DomainProfile("tdunbar@google.com", "Trevor", "Dunbar");
            test.BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified);

            var repo = new ProfileRepository(context);
            await repo.CreateProfileAsync(test);

            var search = await repo.GetProfilesByNameAsync("Trevor", "Dunbar");

            var user = search.First();

            Assert.Equal("tdunbar@google.com", user.Email);
            Assert.Equal("Trevor", user.FirstName);
            Assert.Equal("Dunbar", user.LastName);
        }
    }
}
