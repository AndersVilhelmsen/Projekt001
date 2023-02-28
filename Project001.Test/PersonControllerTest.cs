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
    public class PersonControllerTest
    {
        // Simulate our PersonController ... Because we don't use the database
        private readonly PersonController personController;
        // using Moq
        private readonly Mock<IPersonRepository> repo = new();

        public PersonControllerTest()
        {
            //this.repo = repo;
            personController = new PersonController(repo.Object);
        }


        // should we initialise our objects?

        // Method getPersons
        [Fact]
        public async Task shouldGetAllPersons() {
            // expected 200(List<Person>)

            //Arrange - Data, initialize objects, etc.
            List<Person> persons = new List<Person> {
                new Person { personId = 1, name = "John" },
                new Person { personId = 2, name = "Poul" },
                new Person { personId = 3, name = "George" },
                new Person { personId = 4, name = "Ringo" },
            };

            // We need to mock our Data! How?
            // repo is an object, "that is started" with the method Setup()
            // returns an object, when getPersons() is invoked
            repo.Setup(s => s.getPersons()).ReturnsAsync(persons);


            //Act - Invoke methods
            var result = await personController.GetPerson();

            //Assert - Call Assert, compare output with expected (bool)
            var statuscode = (IStatusCodeActionResult)result;
            Assert.Equal(200, statuscode.StatusCode);
        }

        // Method getPersonById
        [Fact]
        public async Task getPersonByIdTest()
        {
            // expected Person

            //Arrange - Data, initialize objects, etc.
            Person person = new Person { personId = 2, name = "Poul" }; 

            // We need to mock our Data! How?
            // repo is an object, "that is started" with the method Setup()
            // returns an object, when getPersons() is invoked
            repo.Setup(s => s.getPersonById(It.IsAny<int>())).ReturnsAsync(person);

            //Act - Invoke methods
            var result = await personController.GetPersonById(2);

            //Assert - Call Assert, compare output with expected (bool)
            var result2 = (OkObjectResult)result;
            var tt = result2.Value;
            Assert.Equal(person, tt);
        }

        //Method getPersonsByName
        [Fact]
        public async Task getPersonsByNameTest() 
        {
            // Expected List<Person>

            //Arrange
            List<Person> persons = new List<Person> {
                new Person { personId = 1, name = "John" },
                new Person { personId = 2, name = "John" },
                new Person { personId = 3, name = "John" },
            };

            repo.Setup(s => s.getPersonsByName(It.IsAny<string>())).ReturnsAsync(persons);

            //Act
            var result = await personController.GetPersonsByName("test");

            //Assert
            var result2 = (OkObjectResult)result;
            var tt = result2?.Value;
            Assert.Equal(persons, tt);
        }

        [Fact]
        public async Task createPersonTest() 
        {
            //Arrange
            Person person = new Person { personId = 1, name = "John" };

            repo.Setup(s => s.createPerson(It.IsAny<Person>())).ReturnsAsync(person);

            //Act
            var result = await personController.CreatePerson(person);

            //Assert
            var result2 = (CreatedAtActionResult)result;
            var tt = result2.Value;
            var statusCode = (IStatusCodeActionResult)result;
            Assert.Equal(person, tt);
            Assert.Equal(201, statusCode.StatusCode);

        }

        [Fact]
        public async Task updatePersonTest()
        {
            //Arrange
            Person person = new Person { personId = 1, name = "John" };

            repo.Setup(s => s.updatePerson(It.IsAny<Person>())).ReturnsAsync(person);

            //Act
            var result = await personController.UpdatePerson(person);

            //Assert
            var result2 = (CreatedAtActionResult)result;
            var tt = result2.Value;
            var statusCode = (IStatusCodeActionResult)result;
            Assert.Equal(person, tt);
            Assert.Equal(201, statusCode.StatusCode);

        }

        [Fact]
        public async Task deletePersonTest()
        {
            // expected Person

            //Arrange - Data, initialize objects, etc.
            bool deleted = true;

            // We need to mock our Data! How?
            // repo is an object, "that is started" with the method Setup()
            // returns an object, when getPersons() is invoked
            repo.Setup(s => s.deletePerson(It.IsAny<int>())).ReturnsAsync(deleted);

            //Act - Invoke methods
            var result = await personController.DeletePerson(2);

            //Assert - Call Assert, compare output with expected (bool)
            var statuscode = (IStatusCodeActionResult)result;
            Assert.Equal(204, statuscode.StatusCode);
        }
    }
}
