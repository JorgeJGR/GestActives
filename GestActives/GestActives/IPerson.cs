using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestActives
{
    interface IPerson
    {
        string Name { get; set; }
        string Surname { get; set; }
        Company Enterprise { get; set; }
        string FullName();
    }
}