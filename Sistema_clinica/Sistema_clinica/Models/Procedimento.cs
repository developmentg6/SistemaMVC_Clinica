using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models
{
    public class Procedimento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(30, ErrorMessage = "O Nome não pode ter mais de 30 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(100, ErrorMessage = "A Descrição não pode ter mais de 100 caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Tempo estimado de cada sessão")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(5, ErrorMessage = "O Tempo Estimado deve estar no formato hh:mm")]
        public string Tempo_estimado { get; set; }

        [Display(Name = "Valor de cada sessão")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [DisplayFormat(DataFormatString ="R${0:N}")]
        public float Valor_proc { get; set; }

        ProcedimentoBD procedimentoBD = new ProcedimentoBD();
        public string erro { get; set; } = "";



        public List<Procedimento> listaProcedimentos()
        {
            List<Procedimento> lista = new List<Procedimento>();
            try
            {
                lista = procedimentoBD.ListaProcedimentos();
            }
            catch
            {
                erro = procedimentoBD.mensagem;
            }
            return lista;
        }

        public void cadastrar(Procedimento procedimento)
        {
            try
            {
                procedimentoBD.Cadastrar(procedimento);
            }
            catch
            {
                erro = procedimentoBD.mensagem;
            }

        }

        public Procedimento buscar(int id)
        {
            Procedimento proc = new Procedimento();
            try
            {
                proc = procedimentoBD.Buscar(id);
            }
            catch
            {
                erro = procedimentoBD.mensagem;
            }

            return proc;
        }

        public void excluir(int id)
        {
            try
            {
                procedimentoBD.Excluir(id);
            }
            catch
            {
                erro = procedimentoBD.mensagem;
            }
        }

        public void editar(Procedimento procedimento)
        {
            try
            {
                procedimentoBD.Editar(procedimento);
            }
            catch
            {
                erro = procedimentoBD.mensagem;
            }

        }

        public List<Procedimento> filtrarNome(string nome)
        {
            List<Procedimento> lista = new List<Procedimento>();
            try
            {
                lista = procedimentoBD.FiltrarNome(nome);
            }
            catch
            {
                erro = procedimentoBD.mensagem;
            }
            return lista;
        }
        
        public bool existeNome(string nome)
        {
            bool existe = false;
            try
            {
                existe = procedimentoBD.ExisteNome(nome);
            }
            catch
            {
                erro = procedimentoBD.mensagem;
            }
            return existe;

        }

    }
}