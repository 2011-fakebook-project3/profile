using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.StorageModel;

using Xunit;

namespace Fakebook.Profile.DataAccess.StorageModel
{
    public class StorageModelProfileTests
    {

        [Theory]
        [InlineData("test@email.com")]
        [InlineData("random@email.com")]
        [InlineData("Simple@email.com")]
        [InlineData("Other@email.test")]
        [InlineData("subdomianEmai@puppies.test.mail")]
        [InlineData("notcom@puppies.supplies")]
        public void SetEmailShouldWork(string email)
        {
            //arrange
            StorageProfile bf = new StorageProfile();

            //act
            bf.Email = email;

            //assert
            Assert.NotNull(bf.Email);
            Assert.Equal(email, bf.Email);
        }


        //note: maybe this shouldn't work, but
        // it's the model's responsibility for validation 
        [Theory]
        [InlineData("test@email.com")]
        [InlineData("random@email.com")]
        [InlineData("Simple@email.com")]
        [InlineData("Other@email.test")]
        [InlineData("subdomianEmai@puppies.test.mail")]
        [InlineData("notcom@puppies.supplies")]
        public void ChangingEmailShouldWork(string email)
        {
            //arrange
            StorageProfile bf = new StorageProfile();
            bf.Email = "someOtherEmail@email.com";

            //act
            bf.Email = email;

            //assert
            Assert.NotNull(bf.Email);
            Assert.Equal(email, bf.Email);
        }

    }
}
