using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestActives
{
    public class CommercialAgent : Person, INotifyPropertyChanged
    {
        public CommercialAgent() { }

        public CommercialAgent(string name, string surname, Company enterprise, string telephone, string email)
        {
            Name = name;
            Surname = surname;
            Enterprise = enterprise;
            Telephone = telephone;
            Email = email;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
