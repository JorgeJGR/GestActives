using System;
using System.Runtime.Serialization;

namespace GestActives
{

    public class PersonException : Exception
    {
        public PersonException(string message) : base(message) { }
    }
}