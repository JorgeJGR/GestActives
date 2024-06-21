using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestActives
{
    public partial class CompanyView : Window
    {
        private readonly CompanyService _companyService;
        private ObservableCollection<Company> Companies;

        public CompanyView()
        {
            InitializeComponent();
            _companyService = new CompanyService(new GestActivesContext());
            LoadCompanies();
            DataContext = this;

            actualizarButton.IsEnabled = false;
            eliminarButton.IsEnabled = false;
        }

        private void LoadCompanies()
        {
            var companies = _companyService.GetAllCompanies();
            Companies = new ObservableCollection<Company>(companies);
            listCompanyDataGrid.ItemsSource = Companies;
        }

        private void OnFormFieldChanged(object sender, RoutedEventArgs e)
        {
            if (actualizarButton == null || eliminarButton == null)
                return;

            var selectedCompany = listCompanyDataGrid.SelectedItem as Company;

            if (selectedCompany == null)
            {
                actualizarButton.IsEnabled = false;
                eliminarButton.IsEnabled = false;
                return;
            }

            bool anyFieldChanged = (externaChecBox.IsChecked == true ||
                                    telefonoTextBox.Text != selectedCompany.Telephone ||
                                    emailTextBox.Text != selectedCompany.Email);

            actualizarButton.IsEnabled = anyFieldChanged;
            eliminarButton.IsEnabled = anyFieldChanged;

            CommandManager.InvalidateRequerySuggested();
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BuscarNameCompany_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string companyName = nombreTextBox.Text.Trim();

            try
            {
                var company = _companyService.GetCompanyByName(companyName);

                if (company != null)
                {
                    foreach (var item in listCompanyDataGrid.Items)
                    {
                        if (item is Company itemCompany && itemCompany.Name == companyName)
                        {
                            listCompanyDataGrid.SelectedItem = item;
                            break;
                        }
                    }

                    nombreTextBox.IsEnabled = false;
                    telefonoTextBox.Text = company.Telephone;
                    emailTextBox.Text = company.Email;
                    externaChecBox.IsChecked = company.External;
                    eliminarButton.IsEnabled = true;
                    actualizarButton.IsEnabled = true;
                    buscarButton.IsEnabled = false;

                    MessageBox.Show("Compañía encontrada y campos completados.");
                }
                else
                {
                    var result = MessageBox.Show("No se encontró ninguna compañía con ese nombre. ¿Desea añadir una nueva compañía?", "Compañía no encontrada",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Complete los campos para la nueva compañía y haga clic en 'Grabar'.");
                        buscarButton.IsEnabled = false;
                        grabarButton.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar la compañía: {ex.Message}");
            }
        }



        private void BuscarNameCompany_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; 
        }

        private void GrabarCompany_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {

                Company newCompany = new Company
                {
                    Name = nombreTextBox.Text,
                    External = externaChecBox.IsChecked ?? false,
                    Telephone = telefonoTextBox.Text,
                    Email = emailTextBox.Text
                };

                _companyService.InsertCompany(newCompany);
                Companies.Add(newCompany); 
                listCompanyDataGrid.ItemsSource = Companies;


                MessageBox.Show("Compañía añadida con éxito.");
                LimpiarCompany_Executed(sender, null); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al añadir la compañía: {ex.Message}");
            }
        }

        private void GrabarCompany_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !buscarButton.IsEnabled;
        }

        private void LimpiarCompany_Executed(object sender, ExecutedRoutedEventArgs e)
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

            externaChecBox.IsChecked = false;

            grabarButton.IsEnabled = false;
            actualizarButton.IsEnabled = false;
            buscarButton.IsEnabled = true;
            eliminarButton.IsEnabled = false;
            nombreTextBox.IsEnabled = true;
            listCompanyDataGrid.SelectedItem = null;

        }


        private void LimpiarCompany_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EliminarCompany_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            nombreTextBox.IsEnabled = false;
            string companyName = nombreTextBox.Text.Trim();

            var result = MessageBox.Show($"¿Está seguro de que desea eliminar la compañía '{companyName}'?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _companyService.DeleteCompany(companyName);
                    var companyToRemove = Companies.FirstOrDefault(c => c.Name == companyName);
                    if (companyToRemove != null)
                    {
                        Companies.Remove(companyToRemove); 
                        listCompanyDataGrid.ItemsSource = Companies; 
                    }
                    MessageBox.Show("Compañía eliminada con éxito.");
                    LimpiarCompany_Executed(sender, null); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la compañía: {ex.Message}");
                }
            }
        }

        private void ActualizarCompany_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {

                var selectedCompany = (Company)listCompanyDataGrid.SelectedItem;

                Company updatedCompany = new Company
                {
                    IdCompany = selectedCompany.IdCompany,
                    Name = nombreTextBox.Text,
                    External = externaChecBox.IsChecked ?? false,
                    Telephone = telefonoTextBox.Text,
                    Email = emailTextBox.Text
                };


                _companyService.UpdateCompany(updatedCompany);


                var companyToUpdate = Companies.FirstOrDefault(c => c.IdCompany == updatedCompany.IdCompany);
                if (companyToUpdate != null)
                {
                    companyToUpdate.Name = updatedCompany.Name;
                    companyToUpdate.External = updatedCompany.External;
                    companyToUpdate.Telephone = updatedCompany.Telephone;
                    companyToUpdate.Email = updatedCompany.Email;
                }

                MessageBox.Show("Compañía actualizada con éxito.");

                LimpiarCompany_Executed(sender, null);

                eliminarButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la compañía: {ex.Message}");
            }
        }

        private void ActualizarCompany_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var selectedCompany = listCompanyDataGrid.SelectedItem as Company;
            e.CanExecute = (selectedCompany != null &&
                            (externaChecBox.IsChecked == true ||
                            !string.IsNullOrWhiteSpace(telefonoTextBox.Text) ||
                            !string.IsNullOrWhiteSpace(emailTextBox.Text)));
        }

        private void EliminarCompany_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var selectedCompany = listCompanyDataGrid.SelectedItem as Company;
            e.CanExecute = selectedCompany != null;
        }

    }
}