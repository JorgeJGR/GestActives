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

        public void InsertUser(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
        }

        public void UpdateUser(Company company)
        {
            _context.Companies.Update(company);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var company = _context.Companies.Find(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                _context.SaveChanges();
            }
        }
    }
}
