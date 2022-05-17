using CarManagement.Data;
using CarManagement.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management.Cars
{
    public class NewCarMaintenanceViewModel : BaseViewModel
    {
        private readonly Action<MaintenanceRecord> onExit;

        public MaintenanceRecord NewRecord { get; set; }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public NewCarMaintenanceViewModel(MaintenanceRecord record, Action<MaintenanceRecord> onExit)
        {
            this.onExit = onExit;
            
            NewRecord = record;

            SaveCommand = new RelayCommand(onSave);
            CancelCommand = new RelayCommand(onCancel);
        }

        private async void onSave()
        {
            var repo = new MaintenanceRepository();
            if (await repo.SaveRecordAsync(NewRecord))
            {
                onExit(NewRecord);
            }
            else
            {
                onExit(null);
            }
        }

        private void onCancel()
        {
            onExit(null);
        }
    }
}
