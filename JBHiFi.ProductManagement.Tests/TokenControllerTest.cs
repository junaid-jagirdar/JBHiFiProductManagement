using FluentAssertions;
using JBHiFi.ProductManagement.API.Controllers;
using JBHiFi.ProductManagement.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBHiFi.ProductManagement.Tests
{

    [TestClass]
    public class TokenControllerTest
    {
        TokenController controller;
        LoginModel loginModel;

        [TestInitialize]
        public void Initialize()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                    .Build();

            controller = new TokenController(config);
        }

        [TestMethod]
        public void Should_Successful_Create_Token_For_Valid_User()
        {
            loginModel = new LoginModel() { Username = "test", Password = "test123" };
            var result = (ObjectResult)controller.CreateToken(loginModel);
            result.StatusCode.Should().Be(200, "Successfully created token");

        }

        [TestMethod]
        public void Should_Not_Create_Token()
        {
            loginModel = new LoginModel() { Username = "invalid", Password = "invalid" };
            var unAuthResult = (UnauthorizedResult)controller.CreateToken(loginModel);
            unAuthResult.StatusCode.Should().Be(401, "Unauthorized request");
        }
    }
}
