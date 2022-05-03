using Car_Management;
using CarManagement.Data;
using System.Text.Json;

namespace CarManagement.Repos
{
    public class Repository : IRepository
    {
        private readonly string carsFileName = "cars.json";
        private readonly string applicationFolderName = "Car Management";
        public async Task<Car[]> GetCarsAsync()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await Task.Run(async () =>
            {
                var fileName = getFileName();
                using (var stream = File.OpenRead(fileName))
                {
                    return await JsonSerializer.DeserializeAsync<Car[]>(stream);
                }
            });
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> SaveCarsAsync(Car[] cars)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if (!Directory.Exists(getWorkingDirectory()))
                    { 
                        Directory.CreateDirectory(getWorkingDirectory());
                    }

                    if (!File.Exists(getFileName()))
                    {
                        File.Create(getFileName());
                    }

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