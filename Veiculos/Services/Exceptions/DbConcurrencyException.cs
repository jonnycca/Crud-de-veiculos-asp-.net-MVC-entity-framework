using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veiculos.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string mensagem) : base(mensagem)//exceção personalisada para DbConcurrencyException
        {

        }
    }
}
