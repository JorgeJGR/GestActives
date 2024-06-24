using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestActives
{
    public class InternalAssembler : Person, INotifyPropertyChanged
    {
        public InternalAssembler() { }

        public InternalAssembler(string name, string surname, Company enterprise)
        {
            Name = name;
            Surname = surname;
            Enterprise = enterprise;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
