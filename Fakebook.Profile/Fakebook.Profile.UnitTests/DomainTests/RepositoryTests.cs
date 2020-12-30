using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests
{
    public class RepositoryTests
    {
        [Theory]
        [ClassData(typeof(UserData.Create.Valid))]
        public void CreateUser_Valid(User user)
        {
            using var connection = new SqliteConnection
        }

    }
}
