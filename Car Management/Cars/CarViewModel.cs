using CarManagement.Repos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Car_Management.Cars
{
    public class CarViewModel : BaseViewModel
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        public ICommand NewCar { get; set; }

        public CarViewModel()
        {
            getCars();
            NewCar = new RelayCommand(onNewCar);
        }

        private void onNewCar()
        {
            throw new NotImplementedException();
        }

        private async void getCars()
        {
            var repo = new Repository();
            var cars = await repo.GetCarsAsync();
            if (cars != null)
            {
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }
        }
    }
}
