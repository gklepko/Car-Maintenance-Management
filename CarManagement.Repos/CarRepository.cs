using CarManagement.Data;
using System.Text.Json;

namespace CarManagement.Repos
{
    public class CarRepository : ICarRepository
    {
        private readonly string carsFileName = "cars.json";

        public async Task<Car[]> GetCarsAsync()
        {
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
        }

        public async Task<bool> SaveCarAsync(Car car)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if (!Directory.Exists(Helper.GetWorkingDirectory()))
                    { 
                        Directory.CreateDirectory(Helper.GetWorkingDirectory());
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
                        var options = new JsonSerializerOptions(JsonSerializerDefaults.General);
                        options.WriteIndented = true;
                        await JsonSerializer.SerializeAsync(stream, cars, options);
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
                                var options = new JsonSerializerOptions(JsonSerializerDefaults.General);
                                options.WriteIndented = true;
                                await JsonSerializer.SerializeAsync(stream, cars, options);
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


        private string getFileName()
        {
            return Path.Combine(Helper.GetWorkingDirectory(), carsFileName);
        }
    }
}