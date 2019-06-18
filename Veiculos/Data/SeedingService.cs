using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veiculos.Models;
using Veiculos.Models.Enums;
using Veiculos.Services.Exceptions;

namespace Veiculos.Data
{
    public class SeedingService
    {
        private VeiculosContext _context;//registrando o veículo context

        public SeedingService(VeiculosContext context)//construtor que recebe o dbContext indicando que a injeção de dependência deve acontecer
        {
            _context = context;//atribui o valor
        }

        public void Seed()
        {
            if (_context.Veiculo.Any())//verificando se existe dados no veiculo
            {
                return; //banco de dados já está populado
            }
            //se ainda não tem veículos no banco de dados, insere esses veículos a baixo
            Veiculo v1 = new Veiculo(1, "Corsa", "Fiat", 2005, 2006, "1.8", Cores.Amarelo, new DateTime(2006, 01, 01));
            Veiculo v2 = new Veiculo(2, "Uno", "Fiat", 2004, 2004, "1.4", Cores.Preto, new DateTime(2005, 01, 01));

            _context.Veiculo.AddRange(v1, v2);//adiciona os dois veículos
            _context.SaveChanges();//salva as alterações
        }
    }
}
