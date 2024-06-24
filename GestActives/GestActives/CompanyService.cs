using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestActives
{
    class CompanyService
    {
        private readonly GestActivesContext _context;

        public CompanyService(GestActivesContext context)
        {
            _context = context;
        }

        public void InsertCompany(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
        }


        public void UpdateCompany(Company company)
        {
            var existingCompany = _context.Companies.Find(company.IdCompany);
            if (existingCompany != null)
            {
                existingCompany.Name = company.Name;
                existingCompany.External = company.External;
                existingCompany.Telephone = company.Telephone;
                existingCompany.Email = company.Email;
                _context.SaveChanges();
            }
            else
            {
                throw new CompanyException("La compañía no existe.");
            }
        }

        public void DeleteCompany(string companyName)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Name == companyName);
            if (company != null)
            {
                _context.Companies.Remove(company);
                _context.SaveChanges();
            }
        }

        public Company GetOrCreateCompany(string companyName)
        {
            // Definir la compañía por defecto
            var defaultCompany = new Company
            {
                Name = "Sin Nombre",
                External = false,
                Telephone = "",
                Email = ""
            };

            try
            {
                // Buscar la empresa por nombre
                var existingCompany = _context.Companies.FirstOrDefault(c => c.Name == companyName);

                if (existingCompany != null)
                {
                    // Devolver la empresa existente si se encuentra
                    return existingCompany;
                }
                else
                {
                    // Buscar la empresa predeterminada "Sin Nombre"
                    var defaultExistingCompany = _context.Companies.FirstOrDefault(c => c.Name == "Sin Nombre");

                    if (defaultExistingCompany == null)
                    {
                        // Si la empresa "Sin Nombre" no existe, crearla sin asignar manualmente el IdCompany
                        _context.Companies.Add(defaultCompany);
                        _context.SaveChanges();
                        defaultExistingCompany = defaultCompany;
                    }

                    // Mostrar un mensaje emergente indicando que el registro debe ser modificado
                    MessageBox.Show("No se encontró la empresa especificada. Se usará la empresa predeterminada 'Sin Nombre'. Por favor, modifique este registro cuando sea posible.");

                    // Devolver la empresa predeterminada
                    return defaultExistingCompany;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra
                MessageBox.Show($"Error al obtener o crear la empresa: {ex.Message}");
                return null;
            }
        }


        public List<Company> GetAllCompanies() => _context.Companies.ToList();

    }
}