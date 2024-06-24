using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestActives
{
    public class InternalAssembler : Person
    {
        public InternalAssembler() { }

        public InternalAssembler(string name, string surname, Company enterprise)
        {
            Name = name;
            Surname = surname;
            Enterprise = enterprise;
            Discriminator = "InternalAssembler";
        }

    }
}