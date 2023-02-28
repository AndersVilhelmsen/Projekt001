using Microsoft.EntityFrameworkCore;
using Projekt001.Repo.Interfaces;
using Projekt001.Repo.Models_DTO_;
using System.Reflection.Metadata.Ecma335;

namespace Projekt001.Repo.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        // how do I use the database? //ORM

        private readonly DatabaseContext context;// = new();
        public PersonRepository()
        {
            context = new DatabaseContext();
        }

        public PersonRepository(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task<Person> createPerson(Person person)
        {
            context.Person.Add(person);
            await context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> deletePerson(int id)
        {
            Person? person = await context.Person.Include(c=>c.cars).FirstOrDefaultAsync(x=>x.personId==id);
            if (person == null)
            {
                return false;
            }
     
            context.Person.Remove(person);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Person> getPersonById(int id)
        {
            Person? person = await context.Person.Include(c=>c.cars).FirstOrDefaultAsync(p=>p.personId==id);
            if (person == null)
            {
                throw new Exception();
            }

            return person;

        }

        public async Task<List<Person>> getPersonsByName(string name)
        {
            List<Person> persons = await context.Person.Include(c=>c.cars).Where(x=> x.name==name).ToListAsync();
            return persons;
        }

        public async Task<List<Person>> getPersons()
        {
            return await context.Person.Include(c=>c.cars).ToListAsync();
        }

        public async Task<Person> updatePerson(Person person)
        {
            Person? p = context.Person.Include(c => c.cars).FirstOrDefault(x => x.personId == person.personId);
            
            p.name = person.name;
            p.age = person.age;
            p.cars = person.cars;
            //context.Entry(p).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return p;
        }
    }
}


//if (a == b) { xxx; }
//else { yyy; }

//kan omskrives til

//a == b ? xxx : yyy;
    

//x != null ? x : 3;

// kan omskrives til

//x ?? 3; 

//47 + (x ?? 3)
// betyder at hvis x ikke er null, så får vi 47+x, ellers får vi 47+3