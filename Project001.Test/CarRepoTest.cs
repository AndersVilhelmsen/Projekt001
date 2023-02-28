using Microsoft.EntityFrameworkCore;
using Projekt001.Repo;
using Projekt001.Repo.Models_DTO_;
using Projekt001.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project001.Test
{
    public class carRepoTest
    {
        // her er der lidt kode til repo tests

        private DbContextOptions<DatabaseContext> options;
        private DbContextOptions<DatabaseContext> options2;

        private DatabaseContext context;

        public carRepoTest()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "Project001")
              .Options;

            options2 = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "Project002")
              .Options;

            context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Car.Add(new Car { carId = 1, brand = "Skoda" });
            context.Car.Add(new Car { carId = 2, brand = "Lade" });
            context.Car.Add(new Car { carId = 3, brand = "Fiat" });
            context.Car.Add(new Car { carId = 4, brand = "Porche" });
            context.Car.Add(new Car { carId = 5, brand = "Skoda"});
            context.SaveChanges();
        }

        [Fact]
        public async Task GetCarById_CarExists()
        {
            // Arrange
            context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Car.Add(new Car { carId = 1, brand = "Skoda" });
            context.Car.Add(new Car { carId = 2, brand = "Lade" });
            context.Car.Add(new Car { carId = 3, brand = "Fiat" });
            context.Car.Add(new Car { carId = 4, brand = "Porche" });
            context.Car.Add(new Car { carId = 5, brand = "Skoda" });
            context.SaveChanges();
            CarRepository carRepository = new CarRepository(context);

            // Act
            var car = await carRepository.getCarById(1);

            // Assert
            Assert.Equal(1, car.carId);
            Assert.Equal("Skoda", car.brand);
        }

        [Fact]
        public async Task GetCarByBrand_CarExists()
        {
            // Arrange
            context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Car.Add(new Car { carId = 1, brand = "Skoda" });
            context.Car.Add(new Car { carId = 2, brand = "Lade" });
            context.Car.Add(new Car { carId = 3, brand = "Fiat" });
            context.Car.Add(new Car { carId = 4, brand = "Porche" });
            context.Car.Add(new Car { carId = 5, brand = "Skoda" });
            context.SaveChanges();
            CarRepository carRepository = new CarRepository(context);
            // Act
            var cars = await carRepository.getCarsByBrand("Skoda");

            // Assert
            Assert.Equal(2, cars.Count);
        }

        [Fact]
        public async Task GetCars_CarExists()
        {
            // Arrange
            context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Car.Add(new Car { carId = 1, brand = "Skoda" });
            context.Car.Add(new Car { carId = 2, brand = "Lade" });
            context.Car.Add(new Car { carId = 3, brand = "Fiat" });
            context.Car.Add(new Car { carId = 4, brand = "Porche" });
            context.Car.Add(new Car { carId = 5, brand = "Skoda" });
            context.SaveChanges();
            CarRepository carRepository = new CarRepository(context);
            // Act
            var cars = await carRepository.getCars();

            // Assert
            Assert.Equal(5, cars.Count);
        }

        [Fact]
        public async Task CreateCar_CarExists()
        {
            // Arrange
            context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Car.Add(new Car { carId = 1, brand = "Skoda" });
            context.Car.Add(new Car { carId = 2, brand = "Lade" });
            context.Car.Add(new Car { carId = 3, brand = "Fiat" });
            context.Car.Add(new Car { carId = 4, brand = "Porche" });
            context.Car.Add(new Car { carId = 5, brand = "Skoda" });
            context.SaveChanges();
            CarRepository carRepository = new CarRepository(context); Car c = new Car { carId = 0, brand = "BMW"};
            // Act
            var car = await carRepository.createCar(c);

            // Assert
            Assert.Equal(6, car.carId);
            Assert.Equal("BMW", car.brand);

        }

        [Fact]
        public async Task UpdateCar_CarExists()
        {
            // Arrange
            DatabaseContext context2 = new DatabaseContext(options2);
            context2.Database.EnsureDeleted();

            context2.Car.Add(new Car { carId = 1, brand = "Skoda" });
            context2.Car.Add(new Car { carId = 2, brand = "Lade" });
            context2.Car.Add(new Car { carId = 3, brand = "Fiat" });
            context2.Car.Add(new Car { carId = 4, brand = "Porche" });
            context2.Car.Add(new Car { carId = 5, brand = "Skoda" });
            context2.SaveChanges();
            Thread.Sleep(1000);
            CarRepository carRepository = new CarRepository(context2);
            Car c = new Car { carId = 4, brand = "Lamborghini" };
            // Act
            var car = await carRepository.updateCar(c);
            var car2 = await carRepository.getCarById(4);
            // Assert
            Assert.Equal(car2.carId, car.carId);
            Assert.Equal(car2.brand, car.brand);
            }

[Fact]
        public async Task DeleteCar_CarExists()
        {
            // Arrange
            context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Car.Add(new Car { carId = 1, brand = "Skoda" });
            context.Car.Add(new Car { carId = 2, brand = "Lade" });
            context.Car.Add(new Car { carId = 3, brand = "Fiat" });
            context.Car.Add(new Car { carId = 4, brand = "Porche" });
            context.Car.Add(new Car { carId = 5, brand = "Skoda" });
            context.SaveChanges();
            CarRepository carRepository = new CarRepository(context);  
            
            // Act
            var result = await carRepository.deleteCar(5);

            // Assert
            Assert.True(result);

        }

    }
}
