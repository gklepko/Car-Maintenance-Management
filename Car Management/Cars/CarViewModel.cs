using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management.Cars
{
    public class CarViewModel : BaseViewModel
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        public CarViewModel()
        {
            Cars.Add(new Car("Volvo", "C30", 2011));
            Cars.Add(new Car("Toyota", "Camry", 2008));
            Cars.Add(new Car("BMW", "135i", 2011));
            Cars.Add(new Car("Audi", "A4", 2013));
        }
    }
}
