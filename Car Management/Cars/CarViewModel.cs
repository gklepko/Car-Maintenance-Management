﻿using CarManagement.Repos;
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
        private BaseViewModel mainContentArea;
        public BaseViewModel MainContentArea { 
            get
            { 
                return mainContentArea;
            }
            set
            { 
                mainContentArea = value;
                OnPropertyChanged(nameof(MainContentArea));
            } 
        }

        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        public RelayCommand NewCar { get; private set; }
        public RelayCommand DeleteCarCommand { get; private set; }

        public CarViewModel()
        {
            getCars();
            NewCar = new RelayCommand(onNewCar);
            DeleteCarCommand = new RelayCommand(onDelete, () => SelectedCar != null);
        }

        private Car selectedCar;
        public Car SelectedCar 
        {
            get 
            { 
                return selectedCar;
            }

            set 
            {
                selectedCar = value;
                DeleteCarCommand.RaiseCanExecuteChanged();
            }
        }
        private async void onDelete()
        {
            if (SelectedCar != null)
            {  
                var repo = new CarRepository();
                if (await repo.DeleteCarAsync(SelectedCar))
                { 
                    Cars.Remove(SelectedCar);
                    OnPropertyChanged(nameof(Cars));
                }            
            }
        }

        private async void onSaveNewCar(Car car)
        {
            var repo = new CarRepository();
            if (await repo.SaveCarAsync(car))
            { 
                Cars.Add(car);
                OnPropertyChanged(nameof(Cars));
            }
            MainContentArea = null;      
        }

        private async void onNewCar()
        {
            await Task.Yield();
            MainContentArea = new NewCarViewModel(onSaveNewCar, () => MainContentArea = null );
        }

        private async void getCars()
        {
            var repo = new CarRepository();
            var cars = await repo.GetCarsAsync();
            if (cars != null)
            {
                foreach (var car in cars.OrderBy(x => x.Make))
                {
                    Cars.Add(car);
                }
            }

        }
    }
}
