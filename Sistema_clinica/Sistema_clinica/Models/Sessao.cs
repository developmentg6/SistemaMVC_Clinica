using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models
{
    public class Sessao
    {
        public int Id { get; set; }

        public int Id_cliente { get; set; }

        [Display(Name = "Nome do cliente")]
        public string Nome_cliente { get; set; }

        [Display(Name = "CPF do cliente")]
        public string Cpf_cliente { get; set; }

        public int Id_procedimento { get; set; }

        [Display(Name = "Nome do procedimento")]
        public string Nome_procedimento { get; set; }

        public int Id_funcionario { get; set; }

        [Display(Name = "Nome do funcionário")]
        public string Nome_funcionario { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "A Descrição não pode ter mais de 100 caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Total de sessões")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public int Quantidade { get; set; }

        [Display(Name = "Sessões já agendadas")]
        public int Sessoes_agendadas { get; set; }

        public string erro { get; set; } = "";
        SessaoBD sessaoBD = new SessaoBD();

        public List<Sessao> listaSessao()
        {
            List<Sessao> lista = new List<Sessao>();

            try
            {
                lista = sessaoBD.ListaSessao();
            }
            catch
            {
                erro = sessaoBD.mensagem;
            }
            return lista;
        }

        public Sessao buscar(int id)
        {
            Sessao sessao = new Sessao();
            try
            {
                sessao = sessaoBD.Buscar(id);
            }
            catch
            {
                erro = sessaoBD.mensagem;
            }

            return sessao;
        }

        public void buscarIdClientePeloCpf()
        {
            this.Cpf_cliente = this.Cpf_cliente.Remove(11, 1).Remove(7, 1).Remove(3, 1);
            try
            {
                this.Id_cliente = sessaoBD.BuscarClientePeloCpf(this.Cpf_cliente);
            }
            catch{
                erro = sessaoBD.mensagem;
            }
        }

        public void cadastrar(Sessao sessao)
        {
            try
            {
                sessaoBD.Cadastrar(sessao);
            }
            catch
            {
                erro = sessaoBD.mensagem;
            }

        }

        public void editar(Sessao sessao)
        {
            try
            {
                sessaoBD.Editar(sessao);
            }
            catch
            {
                erro = sessaoBD.mensagem;
            }

        }

        public void excluir(int id)
        {
            try
            {
                sessaoBD.Excluir(id);
            }
            catch
            {
                erro = sessaoBD.mensagem;
            }
        }

        public IEnumerable<Sessao> filtrarNomeCliente(string nome)
        {
            return listaSessao().Where(x => x.Nome_cliente.ToLower().Contains(nome.ToLower()));
        }

        public IEnumerable<Sessao> filtrarCpfCliente(string cpf)
        {
            return listaSessao().Where(x => x.Cpf_cliente.Contains(cpf));
        }

        public IEnumerable<Sessao> filtrarProcedimento(string procedimento)
        {
            return listaSessao().Where(x => x.Nome_procedimento.ToLower().Contains(procedimento.ToLower()));
        }

        public IEnumerable<Sessao> filtrarFuncionario(string funcionario)
        {
            return listaSessao().Where(x => x.Nome_funcionario.ToLower().Contains(funcionario.ToLower()));
        }
    }
}