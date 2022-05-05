using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Management
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
