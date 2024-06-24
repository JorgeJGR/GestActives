using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestActives
{
    public class Person : IPerson, INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPerson { get; set; }

        private string name;
        private string surname;
        private Company enterprise;
        private string telephone;
        private string email;

        [Required]
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }

        [Required]
        public string Surname
        {
            get => surname;
            set
            {
                if (surname != value)
                {
                    surname = value;
                    NotifyPropertyChanged(nameof(Surname));
                }
            }
        }

        [Required]
        [ForeignKey("CompanyId")]
        public Company Enterprise
        {
            get => enterprise;
            set
            {
                if (value != null)
                {
                    enterprise = value;
                    NotifyPropertyChanged(nameof(Enterprise));
                }
                else
                {
                    throw new ArgumentException("Enterprise cannot be null.");
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
                    NotifyPropertyChanged(nameof(Telephone));
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
                    NotifyPropertyChanged(nameof(Email));
                }
            }
        }

        public string FullName() => Surname + ", " + Name;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

