using System;
using System.Runtime.Serialization;

namespace GestActives
{
    public class CompanyException : Exception
    {
        public CompanyException(string message) : base(message) { }
    }
}