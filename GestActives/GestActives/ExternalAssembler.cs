using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestActives
{
    public class ExternalAssembler : Person
    {
        public ExternalAssembler() { }

        public ExternalAssembler(string name, string surname, Company enterprise, string telephone)
        {
            Name = name;
            Surname = surname;
            Enterprise = enterprise;
            Telephone = telephone;
            Discriminator = "ExternallAssembler";
        }

    }
}