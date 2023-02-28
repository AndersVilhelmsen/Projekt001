using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt001.Repo.Models_DTO_
{
    public class Person
    {
        public int personId { get; set; }
        public string name { get; set; } = string.Empty;
        public int age { get; set; }
        public List<Car> cars { get; set; } = new List<Car>();
        public Person()
        {
            cars = new List<Car>();
        }
    }
}
