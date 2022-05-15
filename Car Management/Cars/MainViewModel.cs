using CarManagement.Data;
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

            NewCarCommand = new RelayCommand(onNewCar, () => !isInEditMode);
            commands.Add(NewCarCommand);
            DeleteCarCommand = new RelayCommand(onDelete, () => !isInEditMode && SelectedCar != null);
            commands.Add(DeleteCarCommand);
            EditCarCommand = new RelayCommand(onEditCar, () => !isInEditMode && SelectedCar != null);
            commands.Add(EditCarCommand);

            NewRecordCommand = new RelayCommand(newRecord, () => !isInEditMode && SelectedCar != null);
            commands.Add(NewRecordCommand);
            RemoveRecordCommand = new RelayCommand(removeRecord, () => !isInEditMode && SelectedCar != null);
            commands.Add(RemoveRecordCommand);
            EditRecordCommand = new RelayCommand(editRecord, () => !isInEditMode && SelectedCar != null);
            commands.Add(EditRecordCommand);
        }

        // this list is for convinience
        private List<RelayCommand> commands = new List<RelayCommand>();

        #region MainWorking Area
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

        private Stack<BaseViewModel> mainWorkingAreaTracking = new Stack<BaseViewModel>();
        private bool isInEditMode = false;
        private void enterEditMode(BaseViewModel viewModel)
        {
            isInEditMode = true;
            commands.ForEach(c => c.RaiseCanExecuteChanged());
            mainWorkingAreaTracking.Push(MainWorkingArea);
            MainWorkingArea = viewModel;
        }
        private void exitEditMode()
        {
            MainWorkingArea = mainWorkingAreaTracking.Pop();
            isInEditMode = false;
            commands.ForEach(c => c.RaiseCanExecuteChanged());
        }
        #endregion

        #region Cars
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        public RelayCommand NewCarCommand { get; private set; }
        public RelayCommand DeleteCarCommand { get; private set; }
        public RelayCommand EditCarCommand { get; private set; }

        private Car selectedCar;
        public Car SelectedCar 
        {
            get 
            { 
                return selectedCar;
            }

            set 
            {
                if (SelectedCar != value)
                {
                    selectedCar = value;
                    getMaintenanceRecords(SelectedCar);
                    commands.ForEach(c => c.RaiseCanExecuteChanged());
                    OnPropertyChanged(nameof(SelectedCar));
                }
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

        private void onSaveCarInfo(Car car)
        {
            if (car != null)
            {
                if (Cars.Contains(car))
                    Cars.Remove(car);

                Cars.Add(car);
                OnPropertyChanged(nameof(Cars));
            }
            exitEditMode();     
        }

        private void onNewCar()
        {
            enterEditMode(new NewCarViewModel(new Car(), onSaveCarInfo));
        }

        private void onEditCar()
        {
            enterEditMode(new NewCarViewModel(SelectedCar, onSaveCarInfo));
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

        private void newRecord()
        {
            enterEditMode(new NewCarMaintenanceViewModel(new MaintenanceRecord(SelectedCar), onSaveMaintenanceRecord));
        }

        private void editRecord()
        {
            if (SelectedMaintenanceRecord != null)
            {
                enterEditMode(new NewCarMaintenanceViewModel(SelectedMaintenanceRecord, onSaveMaintenanceRecord));
            }
        }

        private async void removeRecord()
        {
            if (SelectedMaintenanceRecord != null)
            {
                var repo = new MaintenanceRepository();
                if (await repo.DeleteRecordAsync(SelectedMaintenanceRecord))
                {
                    SelectedCarMaintenanceRecords.Remove(SelectedMaintenanceRecord);
                    OnPropertyChanged(nameof(SelectedCarMaintenanceRecords));
                }
            }
        }

        private void onSaveMaintenanceRecord(MaintenanceRecord record)
        {
            if (record != null)
            {
                if (SelectedCarMaintenanceRecords != null)
                {
                    if (SelectedCarMaintenanceRecords.Contains(record))
                        SelectedCarMaintenanceRecords.Remove(record);

                    SelectedCarMaintenanceRecords.Add(record);
                    OnPropertyChanged(nameof(SelectedCarMaintenanceRecords));
                }
            }
            exitEditMode();
        }

        public ObservableCollection<MaintenanceRecord> SelectedCarMaintenanceRecords { get; set; }

        private MaintenanceRecord selectedMaintenanceRecord;
        public MaintenanceRecord SelectedMaintenanceRecord 
        {
            get => selectedMaintenanceRecord;
            set
            {
                if (SelectedMaintenanceRecord != value)
                {
                    selectedMaintenanceRecord = value;
                    OnPropertyChanged(nameof(SelectedMaintenanceRecord));
                }
            } 
        }

        private async void getMaintenanceRecords(Car car)
        {
            SelectedCarMaintenanceRecords = new ObservableCollection<MaintenanceRecord>();
            var repo = new MaintenanceRepository();
            var records = await repo.GetRecordsAsync(car);
            if (records != null)
            {
                foreach (var record in records.OrderByDescending(x => x.ServiceDate))
                {
                    SelectedCarMaintenanceRecords.Add(record);
                }
            }
            OnPropertyChanged(nameof(SelectedCarMaintenanceRecords));
        }

        #endregion
    }
}
