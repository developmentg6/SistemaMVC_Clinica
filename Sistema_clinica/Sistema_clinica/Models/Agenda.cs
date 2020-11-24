using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_clinica.Models
{
    public class Agenda
    {
        public int Id { get; set; }

        [Display(Name = "Data/Hora")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Data_Hora { get; set; }

        public string Estado { get; set; }

        [Display(Name = "Pagamento efetuado")]
        public char Pago { get; set; }

        public int Id_sessao { get; set; }

        public int Id_cliente { get; set; }

        [Display(Name = "Cliente")]
        public string Nome_cliente { get; set; }

        [Display(Name = "Cpf do cliente")]
        public string Cpf_cliente { get; set; }

        public int Id_procedimento { get; set; }

        [Display(Name = "Procedimento")]
        public string Nome_procedimento { get; set; }

        public int Id_funcionario { get; set; }

        [Display(Name = "Funcionário")]
        public string Nome_funcionario { get; set; }

        public string erro { get; set; } = "";
        AgendaBD agendaBD = new AgendaBD();


        public List<SelectListItem> listaEstado = new List<SelectListItem>() {
            new SelectListItem { Text = "Agendado", Value = "Agendado" },
            new SelectListItem { Text = "Realizado", Value = "Realizado" },
            new SelectListItem { Text = "Cancelado", Value = "Cancelado" }
        };

        public List<SelectListItem> listaPagamento = new List<SelectListItem>() {
            new SelectListItem { Text = "Não", Value = "N" },
            new SelectListItem { Text = "Sim", Value = "S" }
        };

        public List<Agenda> listaAgenda(int id = 0, string sta = null)
        {
            List<Agenda> lista = new List<Agenda>();

            try
            {
                lista = agendaBD.ListaAgenda(id, sta);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }
            return lista;
        }

        public Agenda buscar(int id)
        {
            Agenda agenda = new Agenda();
            try
            {
                agenda = agendaBD.Buscar(id);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }

            return agenda;
        }

        public void preencherAgendaComSessao(int id_sessao)
        {
            Sessao sessao = new Sessao();
            sessao = sessao.buscar(id_sessao);
            this.Id_sessao = sessao.Id;
            this.Id_cliente = sessao.Id_cliente;
            this.Nome_cliente = sessao.Nome_cliente;
            this.Cpf_cliente = sessao.Cpf_cliente;
            this.Id_procedimento = sessao.Id_procedimento;
            this.Nome_procedimento = sessao.Nome_procedimento;
            this.Id_funcionario = sessao.Id_funcionario;
            this.Nome_funcionario = sessao.Nome_funcionario;
        }


        public void cadastrar(Agenda agenda)
        {
            try
            {
                agendaBD.Cadastrar(agenda);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }

        }

        public void editar(Agenda agenda)
        {
            try
            {
                agendaBD.Editar(agenda);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }

        }

        public void excluir(int id)
        {
            try
            {
                agendaBD.Excluir(id);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }
        }

        public IEnumerable<Agenda> filtrarNomeCliente(string nome)
        {
            List<Agenda> lista = new List<Agenda>();
            try
            {
                lista = agendaBD.FiltrarNomeCliente(nome);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }
            return lista;
        }

        public IEnumerable<Agenda> filtrarCpfCliente(string cpf)
        {
            List<Agenda> lista = new List<Agenda>();
            try
            {
                lista = agendaBD.FiltrarCpfCliente(cpf);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }
            return lista;
        }

        public IEnumerable<Agenda> filtrarProcedimento(string procedimento, int id = 0, string sta = null)
        {
            List<Agenda> lista = new List<Agenda>();
            try
            {
                lista = agendaBD.FiltrarProcedimento(procedimento, id, sta);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }
            return lista;
        }

        public IEnumerable<Agenda> filtrarFuncionario(string funcionario)
        {
            List<Agenda> lista = new List<Agenda>();
            try
            {
                lista = agendaBD.FiltrarFuncionario(funcionario);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }
            return lista;
        }

        public IEnumerable<Agenda> filtrarEstado(string estado)
        {
            List<Agenda> lista = new List<Agenda>();
            try
            {
                lista = agendaBD.FiltrarEstado(estado);
            }
            catch
            {
                erro = agendaBD.mensagem;
            }
            return lista;
        }
    }
}