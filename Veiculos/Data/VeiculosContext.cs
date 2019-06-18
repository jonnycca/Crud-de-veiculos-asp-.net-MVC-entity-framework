using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Veiculos.Models
{
    public class VeiculosContext : DbContext
    {   //classe VeiculoContext que herda da class DbContext para representar a entidade Veiculo no banco de dados
        public VeiculosContext (DbContextOptions<VeiculosContext> options)
            : base(options)
        {
        }

        public DbSet<Veiculo> Veiculo { get; set; }//DbSet do tipo Veiculo para mapear a entidade veículo
    }
}
