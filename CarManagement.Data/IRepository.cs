using Car_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement.Data
{
    public interface IRepository
    {
        Task<Car[]> GetCarsAsync();
        Task<bool> SaveCarAsync(Car car);
        Task<bool> DeleteCarAsync(Car car);
    }
}
