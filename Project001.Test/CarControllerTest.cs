using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Projekt001.API.Controllers;
using Projekt001.Repo.Interfaces;
using Projekt001.Repo.Models_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project001.Test
{
    public class CarControllerTest
    {
        // Simulate our CarController ... Because we don't use the database
        private readonly CarController carController;
        // using Moq
        private readonly Mock<ICarRepository> repo = new();

        public CarControllerTest()
        {
            //this.repo = repo;
            carController = new CarController(repo.Object);
        }

        [Fact]
        public async Task GetPersonsTest()
        {
            // expected 200(List<Car>)

            //Arrange - Data, initialize objects, etc.
            List<Car> cars = new List<Car> {
                new Car { carId = 1, brand = "Skoda" },
                new Car { carId = 2, brand = "Fiat" },
                new Car { carId = 3, brand = "Lada" },
                new Car { carId = 4, brand = "Porche" },
            };

            // We need to mock our Data! How?
            // repo is an object, "that is started" with the method Setup()
            // returns an object, when getPersons() is invoked
            repo.Setup(s => s.getCars()).ReturnsAsync(cars);


            //Act - Invoke methods
            var result = await carController.getCars();

            //Assert - Call Assert, compare output with expected (bool)
            var statuscode = (IStatusCodeActionResult)result;
            Assert.Equal(200, statuscode.StatusCode);
        }

        [Fact]
        public async Task getCarByIdTest()
        {
            //Arrange - Data, initialize objects, etc.
            Car car = new Car { carId = 2, brand = "Skoda" };

            // We need to mock our Data! How?
            // repo is an object, "that is started" with the method Setup()
            // returns an object, when getPersons() is invoked
            repo.Setup(s => s.getCarById(It.IsAny<int>())).ReturnsAsync(car);

            //Act - Invoke methods
            var result = await carController.getCarById(2);

            //Assert - Call Assert, compare output with expected (bool)
            var result2 = (OkObjectResult)result;
            var tt = result2.Value;
            Assert.Equal(car, tt);
        }

        [Fact]
        public async Task getCarsByBrandTest()
        {
            // Expected List<Person>

            //Arrange
            List<Car> cars = new List<Car> {
                new Car { carId = 1, brand = "Skoda" },
                new Car { carId = 2, brand = "Fiat" },
                new Car { carId = 3, brand = "Lada" },
                new Car { carId = 4, brand = "Porche" },
            };

            repo.Setup(s => s.getCarsByBrand(It.IsAny<string>())).ReturnsAsync(cars);

            //Act
            var result = await carController.getCarsByBrand("test");

            //Assert
            var result2 = (OkObjectResult)result;
            var tt = result2?.Value;
            Assert.Equal(cars, tt);
        }

        [Fact]
        public async Task createCarTest()
        {
            //Arrange
            Car car = new Car { carId = 1, brand = "Skoda" };

            repo.Setup(s => s.createCar(It.IsAny<Car>())).ReturnsAsync(car);

            //Act
            var result = await carController.createCar(car);

            //Assert
            var result2 = (CreatedAtActionResult)result;
            var tt = result2.Value;
            var statusCode = (IStatusCodeActionResult)result;
            Assert.Equal(car, tt);
            Assert.Equal(201, statusCode.StatusCode);

        }

        [Fact]
        public async Task updateCarTest()
        {
            //Arrange
            Car car = new Car { carId = 1, brand = "Skoda" };

            repo.Setup(s => s.updateCar(It.IsAny<Car>())).ReturnsAsync(car);

            //Act
            var result = await carController.updateCar(car);

            //Assert
            var result2 = (OkObjectResult)result;
            var tt = result2.Value;
            var statusCode = (IStatusCodeActionResult)result;
            Assert.Equal(car, tt);
            Assert.Equal(200, statusCode.StatusCode);

        }

        [Fact]
        public async Task deleteCarTest()
        {
            // expected Person

            //Arrange - Data, initialize objects, etc.
            //Car car = new Car { carId = 1, brand = "Skoda" };
            bool deleted = true;

            // We need to mock our Data! How?
            // repo is an object, "that is started" with the method Setup()
            // returns an object, when getPersons() is invoked
            repo.Setup(s => s.deleteCar(It.IsAny<int>())).ReturnsAsync(deleted);

            //Act - Invoke methods
            var result = await carController.deleteCar(2);

            //Assert - Call Assert, compare output with expected (bool)
            var result2 = (OkObjectResult)result;
            var tt = result2.Value;
            Assert.Equal(true, tt);
            var statuscode = (IStatusCodeActionResult)result;
            Assert.Equal(200, statuscode.StatusCode);
        }
    }
}
