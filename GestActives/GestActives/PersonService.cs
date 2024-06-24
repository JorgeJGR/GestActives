using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GestActives
{
    class PersonService
    {
        private readonly GestActivesContext _context;

        public List<Person> Persons { get; set; }
        public PersonService(GestActivesContext context)
        {
            _context = context;
        }

        public void InsertPerson(Person person)
        {
            _context.Persons.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            var existingPerson = _context.Persons.Find(person.IdPerson);
            if (existingPerson != null)
            {
                existingPerson.Name = person.Name;
                existingPerson.Surname = person.Surname;
                existingPerson.Enterprise = person.Enterprise;
                existingPerson.Telephone = person.Telephone;
                existingPerson.Email = person.Email;
                existingPerson.Discriminator = person.Discriminator;
                _context.SaveChanges();
            }
            else
            {
                throw new PersonException("La compañía no existe.");
            }
        }

        public void DeletePerson(int PersonId)
        {
            var person = _context.Persons.FirstOrDefault(c => c.IdPerson == PersonId);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }

        public Person GetPersonById(int id) => _context.Persons
                                .Include(p => p.Enterprise)
                                .FirstOrDefault(p => p.IdPerson == id);

        public List<Person> GetAllPerson() => _context.Persons.Include(p => p.Enterprise).ToList();
    }
}