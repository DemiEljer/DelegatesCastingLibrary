using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesCastingLib
{
    internal class DelegateCasterCreationErrorException : Exception
    {
        public DelegateCasterCreationErrorException(string message) : base("The error happened while creating delegate caster: " + message) { }
    }
}
