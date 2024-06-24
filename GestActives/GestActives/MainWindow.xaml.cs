using System.Linq;
using System.Windows;

namespace GestActives
{
    public partial class MainWindow : Window
    {
        private readonly GestActivesContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new GestActivesContext();
            DatabaseInitializer.CreateDatabase();
        }

        private void ProbarButton_Click(object sender, RoutedEventArgs e)
        {
            CompanyView companyView = new CompanyView();
            companyView.Owner = this;
            companyView.ShowDialog();
        }

    }
}
