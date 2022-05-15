using CarManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarManagement.Repos
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        public MaintenanceRepository() { }

        public async Task<bool> DeleteRecordAsync(MaintenanceRecord record)
        {
            if (record.Car == null)
                throw new ArgumentNullException("Car info is expected for each maintenance record");

            return await Task.Run(async () =>
            {
                try
                {
                    List<MaintenanceRecord> records = null;
                    var fileName = getFileName(record.Car);

                    using (var stream = File.OpenRead(fileName))
                    {
                        records = await JsonSerializer.DeserializeAsync<List<MaintenanceRecord>>(stream);
                    }

                    if (records != null)
                    {
                        if (records.RemoveAll(r => r.Key == record.Key) > 0)
                        {
                            using (var stream = File.Create(fileName))
                            {
                                var options = new JsonSerializerOptions(JsonSerializerDefaults.General);
                                options.WriteIndented = true;
                                await JsonSerializer.SerializeAsync(stream, records, options);
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

        public async Task<MaintenanceRecord[]> GetRecordsAsync(Car car)
        {
            return await Task.Run(async () =>
            {
                var fileName = getFileName(car);
                if (File.Exists(fileName))
                {
                    using (var stream = File.OpenRead(fileName))
                    {
                        var records = await JsonSerializer.DeserializeAsync<MaintenanceRecord[]>(stream);
                        Array.ForEach(records, r => r.Car = car);
                        return records;
                    }
                }
                return null;
            });
        }

        public async Task<bool> SaveRecordAsync(MaintenanceRecord record)
        {
            if (record.Car == null)
                throw new ArgumentNullException("Car info is expected for each maintenance record");


            return await Task.Run(async () => 
            {
                try
                {
                    List<MaintenanceRecord> records = null;
                    var fileName = getFileName(record.Car);
                    if (File.Exists(fileName))
                    {
                        using (var stream = File.OpenRead(fileName))
                        {
                            records = await JsonSerializer.DeserializeAsync<List<MaintenanceRecord>>(stream);
                        }
                    }

                    records?.RemoveAll(r => r.Key == record.Key);
                    if (records == null)
                        records = new List<MaintenanceRecord>();
                    records.Add(record);
                    using (var stream = File.Create(getFileName(record.Car)))
                    {
                        var options = new JsonSerializerOptions(JsonSerializerDefaults.General);
                        options.WriteIndented = true;
                        await JsonSerializer.SerializeAsync(stream, records, options);
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private string getFileName(Car car)
        {
            return Path.Combine(Helper.GetWorkingDirectory(), $"{car.Key}.json");
        }
    }
}
