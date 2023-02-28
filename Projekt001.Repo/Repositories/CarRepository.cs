using Microsoft.EntityFrameworkCore;
using Projekt001.Repo.Interfaces;
using Projekt001.Repo.Models_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt001.Repo.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DatabaseContext context;
        public CarRepository()
        {
            context = new();
        }

        public CarRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Car> createCar(Car car)
        {
            context.Car.Add(car);
            await context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> deleteCar(int id)
        {
            Car? car = context.Car.Find(id);
            if (car == null) {
                return false;
            }

            context.Car.Remove(car);
            await context.SaveChangesAsync();
            return true;
            
        }

        public async Task<List<Car>> getCarsByBrand(string brand)
        {
            List<Car> cars = await context.Car.Where(c=>c.brand==brand).ToListAsync();
            return cars;
        }

        public async Task<Car> getCarById(int id)
        {
            Car? car = await context.Car.FindAsync(id);
            if (car == null) throw new Exception();
            return car;
        }

        public async Task<List<Car>> getCars()
        {
            List<Car> cars = await context.Car.ToListAsync();
            return cars;
        }

        public async Task<Car> updateCar(Car car)
        {
            Car tt = await context.Car.FindAsync(car.carId);
            tt.carId = car.carId;
            tt.brand = car.brand;

            //context.Entry(tt).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return car;
        }
    }
}
