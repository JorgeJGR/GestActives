using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestActives
{
    /// <summary>
    /// Lógica de interacción para CompanyView.xaml
    /// </summary>
    public partial class CompanyView : Window
    {
        public ObservableCollection<Company> Companies { get; set; }

        public CompanyView()
        {
            InitializeComponent();
            LoadCompanies();
            DataContext = this;
        }

        private void LoadCompanies()
        {
            using (var context = new GestActivesContext())
            {
                var companies = context.Companies.ToList();
                Companies = new ObservableCollection<Company>(companies);
            }
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
