using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt001.Repo;
using Projekt001.Repo.Interfaces;
using Projekt001.Repo.Models_DTO_;
using Projekt001.Repo.Repositories;

namespace Projekt001.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        ICarRepository repo;
        public CarController(ICarRepository temp)
        {
            repo = temp;
        }

        [HttpGet]
        public async Task<IActionResult> getCars()
        {
            List<Car> cars = await repo.getCars();
            return Ok(cars);
        }

        [HttpPost]
        public async Task<IActionResult> createCar(Car car)
        {
            try
            {
                await repo.createCar(car);
                return CreatedAtAction("createCar", car);
            }
            catch
            {
                return BadRequest("Car not created");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCar(int id)
        {
            try
            {
                await repo.deleteCar(id);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("brand/{brand}")]
        public async Task<IActionResult> getCarsByBrand(string brand)
        {
            List<Car> cars = await repo.getCarsByBrand(brand);
            return Ok(cars);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> getCarById(int id)
        {
            Car car = await repo.getCarById(id);
            return Ok(car);
        }
        
        [HttpPut]
        public async Task<IActionResult> updateCar(Car car)
        {
            Car c = await repo.updateCar(car);
            return Ok(c);
        }
    }
}
