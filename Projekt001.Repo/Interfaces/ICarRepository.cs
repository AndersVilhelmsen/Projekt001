using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt001.Repo.Models_DTO_;

namespace Projekt001.Repo.Interfaces
{
    public interface ICarRepository
    {
        //define methods - CRUD
        public Task<List<Car>> getCars();
        public Task<Car> getCarById(int id);
        public Task<List<Car>> getCarsByBrand(string brand);
        public Task<Car> createCar(Car car);
        public Task<Car> updateCar(Car car);
        public Task<bool> deleteCar(int id);
    }
}
