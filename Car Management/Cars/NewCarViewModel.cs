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
        private readonly Action<Car> onSaveAction;
        private readonly Action onCancelAction;

        public Car NewCar { get; set; }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public NewCarViewModel(Action<Car> onSaveAction, Action onCancelAction)
        {
            NewCar = new Car();
            SaveCommand = new RelayCommand(onSaveCommand);
            CancelCommand = new RelayCommand(onCancelCommand);
            this.onSaveAction = onSaveAction;
            this.onCancelAction = onCancelAction;
        }

        private void onSaveCommand()
        {
            onSaveAction(NewCar);
        }

        private void onCancelCommand()
        {
            onCancelAction();
        }
    }
}
