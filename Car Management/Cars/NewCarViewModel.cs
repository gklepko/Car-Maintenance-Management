using CarManagement.Data;
using CarManagement.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management.Cars
{
    public class NewCarViewModel : BaseViewModel
    {
        private readonly Action<Car> onExit;

        public Car NewCar { get; set; }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public NewCarViewModel(Car car, Action<Car> onExit)
        {
            NewCar = car;
            SaveCommand = new RelayCommand(onSave);
            CancelCommand = new RelayCommand(onCancel);
            this.onExit = onExit;
        }

        private async void onSave()
        {
            var repo = new CarRepository();
            if (await repo.SaveCarAsync(NewCar))
            {
                onExit(NewCar);
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
