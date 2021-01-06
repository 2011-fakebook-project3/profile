using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Moq;
using Fakebook.Profile.RestApi.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace Fakebook.Profile.UnitTests.APITests
{
    public class ProfileControllerTests
    {

        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0


        [Theory]
        //todo: probably change over to using our test data
        //todo: moq a profile repository for when the implementations need one.
        [InlineData("test@test.com")]
        public void GetSpecificProfileWorks(string email)
        {
            // ... ummmmmmm something
            //arrange
            var controller = new ProfileController();

            //act
            var result = controller.Get(email);


            //assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<ViewModel>>(viewResult.ViewData.Model);
        }
    }
}
