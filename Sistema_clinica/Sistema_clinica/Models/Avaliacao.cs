using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [Display(Name = "Data/Hora da avaliação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHora_avaliacao { get; set; }

        public int Id_procedimento { get; set; }

        [Display(Name = "Nome do procedimento")]
        public string Nome_procedimento { get; set; }

        public int Id_cliente { get; set; }

        [Display(Name = "Nome do cliente")]
        public string Nome_cliente { get; set; }

        [Display(Name = "Cpf do cliente")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Preencha corretamente o CPF")]
        public string Cpf_cliente { get; set; }

        [Display(Name = "Observações do cliente")]
        [StringLength(200, ErrorMessage = "A Observação não pode ter mais de 200 caracteres")]
        public string Obs_cliente { get; set; }


        public string erro { get; set; } = "";
        AvaliacaoBD avaliacaoBD = new AvaliacaoBD();



        public List<Avaliacao> listaAvaliacao()
        {
            List<Avaliacao> lista = new List<Avaliacao>();

            try
            {
                lista = avaliacaoBD.ListaAvaliacao();
            }
            catch
            {
                erro = avaliacaoBD.mensagem;
            }
            return lista;
        }

        public Avaliacao buscar(int id)
        {
            Avaliacao avaliacao = new Avaliacao();
            try
            {
                avaliacao = avaliacaoBD.Buscar(id);
            }
            catch
            {
                erro = avaliacaoBD.mensagem;
            }

            return avaliacao;
        }

        public void buscarIdClientePeloCpf()
        {
            this.Cpf_cliente = this.Cpf_cliente.Remove(11, 1).Remove(7, 1).Remove(3, 1);

            try
            {
                this.Id_cliente = avaliacaoBD.BuscarClientePeloCpf(this.Cpf_cliente);
            }
            catch
            {
                erro = avaliacaoBD.mensagem;
            }
        }

        public void cadastrar(Avaliacao avaliacao)
        {
            try
            {
                avaliacaoBD.Cadastrar(avaliacao);
            }
            catch
            {
                erro = avaliacaoBD.mensagem;
            }

        }

        public void editar(Avaliacao avaliacao)
        {
            try
            {
                avaliacaoBD.Editar(avaliacao);
            }
            catch
            {
                erro = avaliacaoBD.mensagem;
            }

        }

        public void excluir(int id)
        {
            try
            {
                avaliacaoBD.Excluir(id);
            }
            catch
            {
                erro = avaliacaoBD.mensagem;
            }
        }

        public IEnumerable<Avaliacao> filtrarNomeCliente(string nome)
        {
            return listaAvaliacao().Where(x => x.Nome_cliente.ToLower().Contains(nome.ToLower()));
        }

        public IEnumerable<Avaliacao> filtrarCpfCliente(string cpf)
        {
            return listaAvaliacao().Where(x => x.Cpf_cliente.Contains(cpf));
        }

        public IEnumerable<Avaliacao> filtrarProcedimento(string procedimento)
        {
            return listaAvaliacao().Where(x => x.Nome_procedimento.ToLower().Contains(procedimento.ToLower()));
        }

    }
}