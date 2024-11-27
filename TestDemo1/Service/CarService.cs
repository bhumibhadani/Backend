using Microsoft.EntityFrameworkCore;
using TestDemo1.Data;
using TestDemo1.Model;

namespace TestDemo1.Service
{
    public class CarService : ICarService
    {
        private readonly CarDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CarService(CarDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            return await _context.Car.ToListAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Car.FindAsync(id);
        }

        public async Task<Car> CreateCarAsync(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> UpdateCarAsync(int id, Car car)
        {
            if (id != car.Id) return false;

            _context.Entry(car).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null) return false;

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> UploadImagesAsync(IFormFile[] files)
        {
            var uploadedFilePaths = new List<string>();

            foreach (var file in files)
            {
                var filePath = Path.Combine("UploadedImages", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                uploadedFilePaths.Add(filePath);
            }

            return uploadedFilePaths;
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }

}
