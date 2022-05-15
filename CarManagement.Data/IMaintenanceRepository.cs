using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement.Data
{
    public interface IMaintenanceRepository
    {
        Task<MaintenanceRecord[]> GetRecordsAsync(Car car);
        Task<bool> SaveRecordAsync(MaintenanceRecord record);
        Task<bool> DeleteRecordAsync(MaintenanceRecord record);
    }
}
