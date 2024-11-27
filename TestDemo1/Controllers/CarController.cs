using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TestDemo1.Data;
using TestDemo1.Model;
using TestDemo1.Service;

namespace TestDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            var cars = await _carService.GetCarsAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            var createdCar = await _carService.CreateCarAsync(car);
            return CreatedAtAction(nameof(GetCar), new { id = createdCar.Id }, createdCar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            var success = await _carService.UpdateCarAsync(id, car);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var success = await _carService.DeleteCarAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile[] files)
        {
            var filePaths = await _carService.UploadImagesAsync(files);
            return Ok(new { FilePath = filePaths });
        }
    }
}