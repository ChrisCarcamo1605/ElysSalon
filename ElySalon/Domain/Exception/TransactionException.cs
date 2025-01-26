using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;

namespace ElySalon.Domain.Exception
{
    internal class TransactionException : System.Exception
    {
        public TransactionException(string message):base(message)
        {
            
        }
        
    }
}
