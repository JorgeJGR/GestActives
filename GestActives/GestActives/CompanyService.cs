using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new Exception("La compañía no existe.");
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


        public List<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public Company GetCompanyByName(string companyName)
        {
            return _context.Companies.FirstOrDefault(c => c.Name == companyName);
        }
    }
}