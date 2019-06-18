using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veiculos.Services.Exceptions
{
    public class DbConnectionErrorMysql : ApplicationException
    {
        public DbConnectionErrorMysql(string mensagem) : base(mensagem)//exceção personalisada para DbConnectionErrorMysql
        {
        }
    }
}
