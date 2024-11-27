using TestDemo1.Model;

namespace TestDemo1.Service
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCarsAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task<Car> CreateCarAsync(Car car);
        Task<bool> UpdateCarAsync(int id, Car car);
        Task<bool> DeleteCarAsync(int id);
        Task<List<string>> UploadImagesAsync(IFormFile[] files);
    }

}
