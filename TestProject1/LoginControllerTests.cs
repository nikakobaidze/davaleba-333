using davaleba_333.Controllers;
using davaleba_333.Models;
using davaleba_333.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class LoginControllerTests
    {
        private LoginController _loginController;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<SignInManager<User>> _mockSignInManager;

        [SetUp]
        public void Setup()
        {
            _mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _mockSignInManager = new Mock<SignInManager<User>>(_mockUserManager.Object,
              Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(), null, null, null, null);
            _loginController = new LoginController(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Test]
        public async Task Login_ValidModel_RedirectsToReturnUrl()
        {
            // Arrange
            var model = new UserFormViewModel
            {
                UserName = "testuser",
                Password = "password"
            };
            var returnUrl = "/home";

            _mockSignInManager.Setup(x => x.PasswordSignInAsync(model.UserName, model.Password, false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await _loginController.Login(model, returnUrl) as LocalRedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(returnUrl, result.Url);
        }

    }
}
