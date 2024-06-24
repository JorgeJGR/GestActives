using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestActives
{
    public enum TipoEmpresa
    {
        Interna,
        Externa
    }

    public class Company : INotifyPropertyChanged
    {
        private int idCompany;
        private string name;
        private bool external;
        private string telephone;
        private string email;

        public Company(int idCompany, string name, bool external, string telephone, string email)
        {
            IdCompany = idCompany;
            Name = name;
            External = external;
            Telephone = telephone;
            Email = email;
        }

        public Company() { }

        public int IdCompany
        {
            get => idCompany;
            set
            {
                idCompany = value;
                if (idCompany == 1)
                {
                    External = false;
                    Name = "Monbake";
                }
                NotifyPropertyChanged("IdCompany");
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public bool External
        {
            get => external;
            set
            {
                if (external != value)
                {
                    external = value;
                    NotifyPropertyChanged("External");
                }
            }
        }

        public string Telephone
        {
            get => telephone;
            set
            {
                if (telephone != value)
                {
                    telephone = value;
                    NotifyPropertyChanged("Telephone");
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public string GetTipoEmpresa() 
            => External ? TipoEmpresa.Externa.ToString() : TipoEmpresa.Interna.ToString();
   
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
