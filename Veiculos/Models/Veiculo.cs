using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using Veiculos.Models.Enums;

namespace Veiculos.Models
{
    public class Veiculo
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Veículo")]
        [Required(ErrorMessage = "{0} obrigatório.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        public string NomeVeiculo { get; set; }

        [Display(Name = "Fabricante")]
        [Required(ErrorMessage = "{0} obrigatório.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        public string NomeFabricante { get; set; }

        [Display(Name = "Fabricação")]
        [BindRequired]
        [Required(ErrorMessage = "{0} obrigatório.")]
        public int AnoFabricacao { get; set; }

        [Display(Name = "Modelo")]
        [BindRequired]
        [Required(ErrorMessage = "{0} obrigatório.")]
        public int AnoModelo { get; set; }

        [Required(ErrorMessage = "{0} obrigatório.")]
        public string Motor { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "{0} obrigatório.")]
        public Cores Cor { get; set; }


        [Display(Name = "Lançamento")]
        [Required(ErrorMessage = "{0} obrigatório.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataLancamento { get; set; }

        public Veiculo()
        {
        }

        public Veiculo(int id, string nomeVeiculo, string nomeFabricante, int anoFabricacao, int anoModelo, string motor, Cores cor, DateTime dataLancamento)
        {
            Id = id;
            NomeVeiculo = nomeVeiculo;
            NomeFabricante = nomeFabricante;
            AnoFabricacao = anoFabricacao;
            AnoModelo = anoModelo;
            Motor = motor;
            Cor = cor;
            DataLancamento = dataLancamento;
        }
    }
}
