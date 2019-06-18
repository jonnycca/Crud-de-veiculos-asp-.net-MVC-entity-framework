using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Veiculos.Models;
using Veiculos.Models.Enums;
using Veiculos.Services;
using Veiculos.Services.Exceptions;

namespace Veiculos.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly VeiculoService _veiculoService;// criando o atributo VeiculoContext que vai servir de base para os metodos de manipulação de dados

        public VeiculosController(VeiculoService veiculoService)//construtor garantindo que vamos receber um objeto do tipo VeiculoService 
        {
            _veiculoService = veiculoService;//atribui ao atributo
        }

        public async Task<IActionResult> Index()//método index do controlador de veiculos, retorna uma lista com todos os veículos cadastrados
        {
            try
            {
                var list = await _veiculoService.FindALLAssincrono();//uma lista recebe os dados do retorno do método que busca e retorna todos os veículos
                return View(list);//passa a lista como parametro para a view
            }
            catch (ApplicationException e)//caso ouva algum erro
            {
                return RedirectToAction(nameof(Erro), new { mensagem = e.Message});//redireciona para a página personalisada de erro
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index(string nomeVeiculo)//método index do tipo POST, quando formos fazer uma pesquisa de veiculo esse método é acionado
        {
            try
            {
                var veiculos = await _veiculoService.FindALLAssincrono();//variavel que recebe todos os veiculos

                if (!String.IsNullOrEmpty(nomeVeiculo))//se a string recebida não é nulla e nem vazia
                {
                    veiculos = veiculos.Where(m => m.NomeVeiculo.Contains(nomeVeiculo)).ToList();//fazemos uma pesquisa usando expressão lambda retornando uma lista de veiculos que contenha a string recebida
                }

                return View(veiculos.ToList());//para a lista como parametro
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Erro), new { mensagem = e.Message });//redireciona para a página personalisada de erro
            }
        }

        public IActionResult Criar()//método usado para criar um novo veiculo
        {
            //operacoes abaixo foram criadas para preencher os dropdownlist de cor, anoDeFabricacao e anoModelo
            var cor = new List<SelectListItem>();//criando uma lista de itens selecionáveis para obter os dados do enum
            cor.Add(new SelectListItem //atribuindo os primeiros dados da lista em branco para garantir que o usário não vá escolher o primeiro dado por engano
            {
                Text = "",
                Value = ""
            });
            foreach (Cores item in Enum.GetValues(typeof(Cores)))//percorre o enum pegando os tipo de cores
            {
                cor.Add(new SelectListItem { Text = Enum.GetName(typeof(Cores), item), Value = item.ToString() }); //adiciona as cores a lista
            }

            var anos = new List<SelectListItem>();//lista para armazenar os números de anos para o dropdownlist do ano de fabricação e modelo
            anos.Add(new SelectListItem
            {
                Text = "",
                Value = ""
            });
            for (int i = 2012; i >= 1980; i--)//percorrendo e adicionando os numeros
            {
                anos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            ViewBag.viewBagAnoModelo = anos;
            ViewBag.viewBagaAnoFabricacao = anos;

            ViewBag.cores = cor;//cria a viewBag com as cores do enum
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Criar(Veiculo veiculo)
        {
            // verificar se o model foi validado, caso o javaScript do usuario esteja desabilitado
            if (!ModelState.IsValid)//se não foi validado, retorna a view de cadastro com o veiculo para poder ser incluido
            {
                var viewModel = veiculo;
                return View(viewModel);//veiculo pass
            }

            await _veiculoService.InserirAssincrono(veiculo);//insere o veiculo passado como parametro
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remover(int? id)//método para remover veiculo pelo id GET
        {
            if ( id == null)//se id passado é nullo
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = "Id não encontrado!"});//redireciona para a página personalisada de erro
            }

            var obj = await _veiculoService.PesquisaPorIdAssincrono(id.Value);//pesquisa um veiculo com o id passado
            if (obj == null)//se objeto for nullo, não encontrou nenhum objeto com o id informado
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = "Nenhum veículo com o id informado!" });//redireciona para a página personalisada de erro
            }

            return View(obj);//retorna o objeto a ser removido
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)//método para remover veiculo pelo id POST
        {
            await _veiculoService.RemoverAssincrono(id);//chama método para remover veiculo pelo id
            return RedirectToAction(nameof(Index));//redireciona para a página principal
        }

        public async Task<IActionResult> Detalhes(int? id)//método que mostra os detalhes do veiculo
        {
            if (id == null)//se id passado for nullo
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = "Id não fornecido" });//redireciona para a página pesonalisada de erro
            }

            var obj = await _veiculoService.PesquisaPorIdAssincrono(id.Value);//pesquisa um objeto com o id informado
            if (obj == null)//se o objeto está nullo, ou seja, não encontrou um veiculo com o id informado
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = "Nenhum veículo com o id informado!" });//redireciona para a página pesonalisada de erro
            }

            return View(obj);//retorna objeto a ser removido
        }


        public async Task<IActionResult> Editar(int? id)//editar veiculo  GET
        {
            if (id == null)//verifica se id informado é nullo
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = "Id não fornecido" });//redireciona para a página pesonalisada de erro
            }

            var obj = await _veiculoService.PesquisaPorIdAssincrono(id.Value);//pesquisa veiculo pelo id informado
            if (obj == null)//se o obj é nullo, não encontrou um objeto com o id informado
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = "Nenhum veículo com o id informado!" });//redireciona para a página pesonalisada de erro
            }

            var cor = new List<SelectListItem>();//lista de SelectListItem para o dropdownlist de cores
            cor.Add(new SelectListItem//atribuindo os primeiros dados da lista em branco para garantir que o usário não vá escolher o primeiro dado por engano
            {
                Text = "",
                Value = ""
            });
            foreach (Cores item in Enum.GetValues(typeof(Cores)))//percorre o enum pegando os tipo de cores
            {
                cor.Add(new SelectListItem { Text = Enum.GetName(typeof(Cores), item), Value = item.ToString() });//adiciona as cores a lista
            }

            var anos = new List<SelectListItem>();//lista para armazenar os números de anos para o dropdownlist do ano de fabricação e modelo
            anos.Add(new SelectListItem
            {
                Text = "",
                Value = ""
            });
            for (int i = 2012; i >= 1980; i--)//percorrendo e adicionando os numeros
            {
                anos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            ViewBag.viewBagAnoModelo = anos;
            ViewBag.viewBagaAnoFabricacao = anos;

            ViewBag.cores = cor;//cria a viewBag de cores

            return View(obj);//retorna o objeto
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Editar(int id, Veiculo veiculo)//método editar POST
        {
            // verificar se o model foi validado, caso o javaScript do usuario esteja desabilitado
            if (!ModelState.IsValid)//se não foi validado, retorna a view de editar com o veiculo para poder ser editado
            {
                var viewModel = veiculo;
                return View(viewModel);
            }
            if (id != veiculo.Id)//verifica se id informado é diferente do id do veiculo
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = "Id incompatível" });//redireciona para a página pesonalisada de erro
            }
            try
            {
                await _veiculoService.AtualizarAssincrono(veiculo);//atualiza os dados do veiculo
                return RedirectToAction(nameof(Index));//retorna para a página principal
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = e.Message });
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Erro), new { Mensagem = e.Message });
            }
        }

        public IActionResult Erro(string mensagem)//metodo que recebe as mensagens de erro e encaminha para a view personalisada de erro
        {
            var viewModel = new ErrorViewModel//cria um novo objeto do tipo ErrorViewModel atribuindo a string de erro 
            {
                Menssagem = mensagem,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        public IActionResult Sobre()//ação sobre
        {
            return View();
        }
    }
}