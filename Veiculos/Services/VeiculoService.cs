using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veiculos.Models;
using Veiculos.Services.Exceptions;

namespace Veiculos.Services
{
    public class VeiculoService//classe de acesso a dados
    {
        private readonly VeiculosContext _context;//criando o atributo VeiculoContext que vai servir de base para os metodos de manipulação de dados

        public VeiculoService(VeiculosContext context)//método construtor que recebe um objeto do tipo VeiculoContext para garantir que a integridade com a entidade
        {
            _context = context;//atribuí o valor ao atributo context
        }

        public async Task<List<Veiculo>> FindALLAssincrono() //metodo que retorna os veículos
        {
            try
            {
                return await _context.Veiculo.OrderBy(x => x.NomeVeiculo).ToListAsync();//returna os veiculos ordenando pelo nome
            }
            catch (ApplicationException e)
            {
                throw new DbConnectionErrorMysql("Erro ao obter os dados do banco de dados: " + e.Message);//casso ouve um erro envia uma mensagem
            }
        }

        public async Task InserirAssincrono(Veiculo obj) //método para inserir um veículo na base de dados
        {
            _context.Add(obj);//adiciona o objeto veículo
            await _context.SaveChangesAsync();//salva as alterações do objeto veículo
        }

        public async Task<Veiculo> PesquisaPorIdAssincrono(int id)//método que pesquisa pelo Id do veiculo
        {
            return await _context.Veiculo.FirstOrDefaultAsync(obj => obj.Id == id);//expressão lambda que faz uma busca na entidade veiculo pelo id
        }

        public async Task RemoverAssincrono(int id)//método que remove pelo id
        {
            var obj = _context.Veiculo.Find(id);//pesquisa um veiculo na entidade veiculo com o id igual ao id recebido e atribui a variavel obj
            _context.Veiculo.Remove(obj); //remove o obj
            await _context.SaveChangesAsync();//atualiza a entidade
        }

        public async Task AtualizarAssincrono(Veiculo obj)//método que recebe o veículo e atualiza os dados dele
        {
            bool existVeiculo = await _context.Veiculo.AnyAsync(x => x.Id == obj.Id); //método que retorna se encontrou um objeto equivalente a expressão lambda
            if (!existVeiculo)//se não retornou true na expressão acima, arremessa erro
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);//atualiza a entidade com os dados do veículo passado por parametro
                await _context.SaveChangesAsync();//salva as alterações
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);//arremesa erro pq ouve erro ao atualizar contato
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.Message);//arremesa erro em geral
            }
        }
    }
}
