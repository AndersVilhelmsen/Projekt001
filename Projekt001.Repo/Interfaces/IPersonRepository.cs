using Projekt001.Repo.Models_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt001.Repo.Interfaces
{
    public interface IPersonRepository
    {
        //define methods - CRUD
        public Task<List<Person>> getPersons();
        public Task<Person> getPersonById(int id);
        public Task<List<Person>> getPersonsByName(string name);
        public Task<Person> createPerson(Person person);
        public Task<Person> updatePerson(Person person);
        public Task<bool> deletePerson(int id);
    }
}