using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Profile.Domain;
using Moq;

using Xunit;

namespace Fakebook.Profile.UnitTests.DomainTests
{
    public class ProfileModelTests
    {
        [Theory]
        [InlineData("first", "last")]
        public void NameReturnsProperFormat(string first, string last)
        {
            //arrange
            DomainProfile profile = new DomainProfile();

            //act 

            //assert
        }
    }
}
