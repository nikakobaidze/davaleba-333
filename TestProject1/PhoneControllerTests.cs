using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using davaleba_333.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using davaleba_333.Data;
using davaleba_333.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestProject1
{
    internal class PhoneControllerTests
    {
        private PhoneController _phoneController;
        private Mock<ApplicationDbContext> _mockDbContext;

        [SetUp]
        public void Setup()
        {
            _mockDbContext = new Mock<ApplicationDbContext>();
            _phoneController = new PhoneController(_mockDbContext.Object);
        }
        [Test]
        public void GetPhones_ReturnsListOfPhones()
        {
            // Arrange
            var phones = new List<Phone> { new Phone { ID = 1, Name = "Phone 1", Price = 100 } };
            _mockDbContext.Setup(x => x.Phones).Returns((Microsoft.EntityFrameworkCore.DbSet<Phone>)phones.AsQueryable());

            // Act
            var result = _phoneController.GetPhones();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(phones, okResult.Value);
        }
        [Test]
        public void CreatePhone_ReturnsCreatedPhone()
        {
            // Arrange
            var phone = new Phone { ID = 1, Name = "Phone 1", Price = 100 };

            // Act
            var result = _phoneController.CreatePhone(phone);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(phone, okResult.Value);
        }

    }
}
