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
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            getCars();
            NewCarCommand = new RelayCommand(onNewCar);
            DeleteCarCommand = new RelayCommand(onDelete, () => SelectedCar != null);
            NewRecordCommand = new RelayCommand(newRecord, () => SelectedCar != null);
            RemoveRecordCommand = new RelayCommand(removeRecord, () => SelectedCar != null);
            EditRecordCommand = new RelayCommand(editRecord, () => SelectedCar != null);
        }

        private BaseViewModel mainWorkingArea;
        public BaseViewModel MainWorkingArea { 
            get
            { 
                return mainWorkingArea;
            }
            set
            { 
                mainWorkingArea = value;
                OnPropertyChanged(nameof(MainWorkingArea));
            } 
        }

        #region Cars
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        public RelayCommand NewCarCommand { get; private set; }
        public RelayCommand DeleteCarCommand { get; private set; }

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
                NewRecordCommand.RaiseCanExecuteChanged();
                RemoveRecordCommand.RaiseCanExecuteChanged();
                EditRecordCommand.RaiseCanExecuteChanged();
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
            MainWorkingArea = null;      
        }

        private async void onNewCar()
        {
            await Task.Yield();
            MainWorkingArea = new NewCarViewModel(onSaveNewCar, () => MainWorkingArea = null);
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

        #endregion


        #region Maintenance

        public RelayCommand NewRecordCommand { get; private set; }
        public RelayCommand RemoveRecordCommand { get; private set; }
        public RelayCommand EditRecordCommand { get; private set; }

        private async void newRecord()
        {
            await Task.Yield();
            MainWorkingArea = new NewCarMaintenanceViewModel(onSaveNewMaintenanceRecord, () => MainWorkingArea = null);
            
        }

        private async void editRecord()
        {
            await Task.Yield();
            
        }

        private async void removeRecord()
        {
            await Task.Yield();
            
        }

        private async void onSaveNewMaintenanceRecord(MaintenanceRecord record)
        {
            if (SelectedCar != null)
            { 
                SelectedCar.AddMaintenanceRecord(record);
                var repo = new CarRepository();
                await repo.SaveCarAsync(SelectedCar);
                MainWorkingArea = null;
            }
        }


        #endregion
    }
}
