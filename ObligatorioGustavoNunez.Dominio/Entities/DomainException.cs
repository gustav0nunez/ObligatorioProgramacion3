using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioGustavoNunez.Dominio.Entities
{
    public class DomainException : Exception
    {
        public DomainException(string mensaje): base(mensaje)
           { 
        
        
        }
    }
}
