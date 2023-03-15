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
    public class personRepoTest
    {
        // her er der lidt kode til repo tests

        private DbContextOptions<DatabaseContext> options;
        private DatabaseContext context;

        public personRepoTest()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "Project001")
              .Options;

            //context = new DatabaseContext(options);
            //context.Database.EnsureDeleted();

            //context.Person.Add(new Person { personId = 1, name = "John", age=22 });
            //context.Person.Add(new Person { personId = 2, name = "Poul", age=23 });
            //context.Person.Add(new Person { personId = 3, name = "George", age=24 });
            //context.Person.Add(new Person { personId = 4, name = "Ringo", age=25 });
            //context.Person.Add(new Person { personId = 5, name = "John", age = 22 });
            //context.SaveChanges();
        }

        [Fact]
        public async Task GetPersonById_PersonExists()
        {
            // Arrange
            DatabaseContext context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Person.Add(new Person { personId = 1, name = "John", age = 22 });
            context.Person.Add(new Person { personId = 2, name = "Poul", age = 23 });
            context.Person.Add(new Person { personId = 3, name = "George", age = 24 });
            context.Person.Add(new Person { personId = 4, name = "Ringo", age = 25 });
            context.Person.Add(new Person { personId = 5, name = "John", age = 22 });
            context.SaveChanges();
            PersonRepository personRepository = new PersonRepository(context);

            // Act
            var person = await personRepository.getPersonById(1);

            // Assert
            Assert.Equal(1, person.personId);
            Assert.Equal("John", person.name);
            Assert.Equal(22, person.age);
        }

        [Fact]
        public async Task GetPersonByName_PersonExists()
        {
            // Arrange
            DatabaseContext context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Person.Add(new Person { personId = 1, name = "John", age = 22 });
            context.Person.Add(new Person { personId = 2, name = "Poul", age = 23 });
            context.Person.Add(new Person { personId = 3, name = "George", age = 24 });
            context.Person.Add(new Person { personId = 4, name = "Ringo", age = 25 });
            context.Person.Add(new Person { personId = 5, name = "John", age = 22 });
            context.SaveChanges();
            PersonRepository personRepository = new PersonRepository(context);
            // Act
            var persons = await personRepository.getPersonsByName("John");

            // Assert
            Assert.Equal(2, persons.Count);
        }

        [Fact]
        public async Task GetPersons_PersonExists()
        {
            // Arrange
            DatabaseContext context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Person.Add(new Person { personId = 1, name = "John", age = 22 });
            context.Person.Add(new Person { personId = 2, name = "Poul", age = 23 });
            context.Person.Add(new Person { personId = 3, name = "George", age = 24 });
            context.Person.Add(new Person { personId = 4, name = "Ringo", age = 25 });
            context.Person.Add(new Person { personId = 5, name = "John", age = 22 });
            context.SaveChanges();
            PersonRepository personRepository = new PersonRepository(context);

            // Act
            var persons = await personRepository.getPersons();

            // Assert
            Assert.Equal(5, persons.Count);
        }

        [Fact]
        public async Task CreatePersons_PersonExists()
        {
            // Arrange
            DatabaseContext context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Person.Add(new Person { personId = 1, name = "John", age = 22 });
            context.Person.Add(new Person { personId = 2, name = "Poul", age = 23 });
            context.Person.Add(new Person { personId = 3, name = "George", age = 24 });
            context.Person.Add(new Person { personId = 4, name = "Ringo", age = 25 });
            context.Person.Add(new Person { personId = 5, name = "John", age = 22 });
            context.SaveChanges();
            PersonRepository personRepository = new PersonRepository(context); Person p = new Person { personId = 0, name = "Beatles", age = 26 };
            // Act
            var person = await personRepository.createPerson(p);

            // Assert
            Assert.Equal(6, person.personId);
            Assert.Equal("Beatles", person.name);
            Assert.Equal(26, person.age);

        }

        [Fact]
        public async Task UpdatePersons_PersonExists()
        {
            // Arrange
            DatabaseContext context = new DatabaseContext(options);
            context.Database.EnsureDeleted();

            context.Person.Add(new Person { personId = 1, name = "John", age = 22 });
            context.Person.Add(new Person { personId = 2, name = "Poul", age = 23 });
            context.Person.Add(new Person { personId = 3, name = "George", age = 24 });
            context.Person.Add(new Person { personId = 4, name = "Ringo", age = 25 });
            context.Person.Add(new Person { personId = 5, name = "John", age = 22 });
            context.SaveChanges();

            PersonRepository personRepository = new PersonRepository(context);
            Person p = new Person { personId = 4, name = "Ringo Star", age = 24 };
            // Act
            var person = await personRepository.updatePerson(p);
            //var person2 = await personRepository.getPersonById(4);
            // Assert
            Assert.Equal(4, person.personId);
            Assert.Equal("Ringo Star", person.name);
            Assert.Equal(24, person.age);
            //Assert.Equal(person2.personId, person.personId);
            //Assert.Equal(person2.name, person.name);
            //Assert.Equal(person2.age, person.age);

        }

        [Fact]
        public async Task DeletePersons_PersonExists()
        {
            // Arrange
            DatabaseContext context2 = new DatabaseContext(options);
            context2.Database.EnsureDeleted();

            context2.Person.Add(new Person { personId = 1, name = "John", age = 22 });
            context2.Person.Add(new Person { personId = 2, name = "Poul", age = 23 });
            context2.Person.Add(new Person { personId = 3, name = "George", age = 24 });
            context2.Person.Add(new Person { personId = 4, name = "Ringo", age = 25 });
            context2.Person.Add(new Person { personId = 5, name = "John", age = 22 });
            context2.SaveChanges();
            PersonRepository personRepository = new PersonRepository(context2);

            // Act
            var result = await personRepository.deletePerson(5);

            // Assert
            Assert.Equal(true, result);

        }

    }
}
