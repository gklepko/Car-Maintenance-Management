using Car_Management.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            CurrentViewModel = new MainViewModel();
        }
        public BaseViewModel CurrentViewModel { get; set; }
    }
}
