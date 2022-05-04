using Car_Management;
using CarManagement.Data;
using System.Text.Json;

namespace CarManagement.Repos
{
    public class CarRepository : ICarRepository
    {
        private readonly string carsFileName = "cars.json";
        private readonly string applicationFolderName = "Car Management";
        public async Task<Car[]> GetCarsAsync()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await Task.Run(async () =>
            {
                var fileName = getFileName();
                if (File.Exists(fileName))
                {
                    using (var stream = File.OpenRead(fileName))
                    {
                        return await JsonSerializer.DeserializeAsync<Car[]>(stream);
                    }
                }
                return null;
            });
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> SaveCarAsync(Car car)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if (!Directory.Exists(getWorkingDirectory()))
                    { 
                        Directory.CreateDirectory(getWorkingDirectory());
                    }

                    List<Car> cars = null;
                    var fileName = getFileName();
                    if (File.Exists(fileName))
                    {
                        using (var stream = File.OpenRead(fileName))
                        { 
                            cars = await JsonSerializer.DeserializeAsync<List<Car>>(stream);
                        }
                    }

                    cars?.RemoveAll(c => c.Key == car.Key);
                    
                    if (cars == null)
                        cars = new List<Car>();
                    cars.Add(car);
                    using (var stream = File.Create(getFileName()))
                    {
                        await JsonSerializer.SerializeAsync(stream, cars);
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteCarAsync(Car car)
        {
            return await Task.Run(async () =>
            {
                try 
                {
                    List<Car> cars = null;
                    var fileName = getFileName();

                    using (var stream = File.OpenRead(fileName))
                    {
                        cars = await JsonSerializer.DeserializeAsync<List<Car>>(stream);
                    }

                    if (cars != null)
                    {
                        if (cars.RemoveAll(c => c.Key == car.Key) > 0)
                        {
                            using (var stream = File.Create(fileName))
                            { 
                                await JsonSerializer.SerializeAsync(stream, cars);
                            }
                        }
                    }
                    return true; 
                }
                catch 
                { 
                    return false; 
                }
            });
        }

        private string getWorkingDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), applicationFolderName);
        }
        private string getFileName()
        {
            return Path.Combine(getWorkingDirectory(), carsFileName);
        }

       
    }
}