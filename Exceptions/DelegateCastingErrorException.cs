using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesCastingLib
{
    /// <summary>
    /// Ошибка кастинга делегата
    /// </summary>
    public class DelegateCastingErrorException : Exception
    {
        public DelegateCastingErrorException(string message) : base("The error happened while executing delegate caster: " + message) { }
    }
}
