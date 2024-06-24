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
    /// Lógica de interacción para PersonView.xaml
    /// </summary>
    public partial class PersonView : Window
    {
        private readonly PersonService _personService;
        private readonly CompanyService _companyService;
        private ObservableCollection<Person> Persons;

        public PersonView()
        {
            InitializeComponent();
            _personService = new PersonService(new GestActivesContext());
            _companyService = new CompanyService(new GestActivesContext());
            tipoPersonaCombobox.DataContext = new List<string>() {"Montador Propio", "Montador Externo", "Comercial" };
            LoadPersons();
            DataContext = this;

            actualizarButton.IsEnabled = false;
            eliminarButton.IsEnabled = false;
        }

        private void LoadPersons()
        {
            var persons = _personService.GetAllPerson();
            Persons = new ObservableCollection<Person>(persons);
            listPersonDataGrid.ItemsSource = Persons;
        }

        private void BuscarIdPerson_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int personId = int.Parse(identificadorTextBox.Text);

            try
            {
                var persona = _personService.GetPersonById(personId);

                if (persona != null)
                {
                    
                    foreach (var item in listPersonDataGrid.Items)
                    {
                        if (item is Person itemPerson && itemPerson.IdPerson == personId)
                        {
                            listPersonDataGrid.SelectedItem = item;
                            break;
                        }
                    }

                    identificadorTextBox.IsEnabled = false;
                    nombreTextBox.Text = persona.Name;
                    apellidosTextBox.Text = persona.Surname;
                    empresaTextBox.Text = persona.CompanyName;
                    telefonoTextBox.Text = persona.Telephone;
                    emailTextBox.Text = persona.Email;
                    tipoPersonaCombobox.SelectedItem = persona.Discriminator;
                }
                else
                {
                    var result = MessageBox.Show("No se encontró ninguna Persona con ese identificador. ¿Desea añadir una nueva Persona?",
                                                 "Persona no encontrada",
                                                 MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Complete los campos para la nueva Persona y haga clic en 'Grabar'.");
                        buscarButton.IsEnabled = false;
                        grabarButton.IsEnabled = true;
                    }
                }
            }
            catch (PersonException ex)
            {
                MessageBox.Show($"Error al buscar la Persona: {ex.Message}");
            }
        }

        private void BuscarIdPerson_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnFormFieldChanged(object sender, RoutedEventArgs e)
        {
            if (actualizarButton == null || eliminarButton == null)
                return;

            var selectedPerson = listPersonDataGrid.SelectedItem as Person;

            if (selectedPerson == null)
            {
                actualizarButton.IsEnabled = false;
                eliminarButton.IsEnabled = false;
                return;
            }

            bool anyFieldChanged = (telefonoTextBox.Text != selectedPerson.Telephone ||
                                    emailTextBox.Text != selectedPerson.Email ||
                                    tipoPersonaCombobox.SelectedItem != selectedPerson.Discriminator);

            actualizarButton.IsEnabled = anyFieldChanged;
            eliminarButton.IsEnabled = anyFieldChanged;
            identificadorTextBox.IsEnabled = false;

            CommandManager.InvalidateRequerySuggested();
        }


        private void OnTipoPersonaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (actualizarButton == null || eliminarButton == null)
                return;

            var selectedPerson = listPersonDataGrid.SelectedItem as Person;

            if (selectedPerson == null)
            {
                actualizarButton.IsEnabled = false;
                eliminarButton.IsEnabled = false;
                return;
            }

            bool anyFieldChanged = (telefonoTextBox.Text != selectedPerson.Telephone ||
                                    emailTextBox.Text != selectedPerson.Email ||
                                    tipoPersonaCombobox.SelectedItem != selectedPerson.Discriminator);

            actualizarButton.IsEnabled = anyFieldChanged;
            eliminarButton.IsEnabled = anyFieldChanged;

            CommandManager.InvalidateRequerySuggested();
        }


        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void GrabarPerson_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                string nombre = nombreTextBox.Text;
                string apellidos = apellidosTextBox.Text;
                string nombreEmpresa = empresaTextBox.Text;
                string telefono = telefonoTextBox.Text;
                string email = emailTextBox.Text;
                string tipoPersona = tipoPersonaCombobox.SelectedItem?.ToString();

                Company empresa = _companyService.GetOrCreateCompany(nombreEmpresa);

                if (empresa == null)
                {
                    MessageBox.Show("Error al obtener o crear la empresa. No se puede proceder.");
                    return;
                }

                Person nuevaPersona = null;

                switch (tipoPersona)
                {
                    case "Montador Propio":
                        nuevaPersona = new InternalAssembler(nombre, apellidos, empresa);
                        break;
                    case "Montador Externo":
                        nuevaPersona = new ExternalAssembler(nombre, apellidos, empresa, telefono);
                        break;
                    case "Comercial":
                        nuevaPersona = new CommercialAgent(nombre, apellidos, empresa, telefono, email);
                        break;
                    default:
                        throw new InvalidOperationException("Tipo de persona seleccionado no válido.");
                }

                _personService.InsertPerson(nuevaPersona);


                Persons.Add(nuevaPersona);
                listPersonDataGrid.ItemsSource = Persons;

                MessageBox.Show("Persona añadida con éxito.");
                LimpiarPerson_Executed(sender, null); 
            }
            catch (PersonException ex)
            {
                MessageBox.Show($"Error al añadir la persona: {ex.Message}");
            }
        }

        private void GrabarPerson_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !buscarButton.IsEnabled;
        }

        private void LimpiarPerson_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var control in formularioStackPanel.Children)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is StackPanel innerStackPanel)
                {
                    foreach (var innerControl in innerStackPanel.Children)
                    {
                        if (innerControl is TextBox innerTextBox)
                        {
                            innerTextBox.Clear();
                        }
                    }
                }
            }

            identificadorTextBox.IsEnabled = true;

            grabarButton.IsEnabled = false;
            actualizarButton.IsEnabled = false;
            buscarButton.IsEnabled = true;
            eliminarButton.IsEnabled = false;
            nombreTextBox.IsEnabled = true;
            listPersonDataGrid.SelectedItem = null;

        }

        private void LimpiarPerson_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}