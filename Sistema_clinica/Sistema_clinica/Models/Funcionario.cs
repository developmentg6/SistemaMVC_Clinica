using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models
{
    public class Funcionario
    {

        [Display(Name = "Id do funcionário")]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(60, ErrorMessage = "O Nome não pode ter mais de 60 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(30, ErrorMessage = "O Cargo não pode ter mais de 30 caracteres")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Cpf { get; set; }

        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido")]
        [StringLength(60, ErrorMessage = "O Email não pode ter mais de 60 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(80, ErrorMessage = "A Rua não pode ter mais de 80 caracteres")]
        public string Rua { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(30, ErrorMessage = "O Bairro não pode ter mais de 30 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(30, ErrorMessage = "A Cidade não pode ter mais de 30 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Cep { get; set; }

        FuncionarioBD funcionarioBD = new FuncionarioBD();
        public string erro { get; set; } = "";


        public List<Funcionario> listaFuncionarios()
        {
            List<Funcionario> lista = new List<Funcionario>();
            try
            {
                lista = funcionarioBD.ListaFuncionarios();
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }
            return lista;
        }

        public void cadastrar(Funcionario funcionario)
        {
            try
            {
                funcionarioBD.Cadastrar(funcionario);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }

        }

        public Funcionario buscar(int id)
        {
            Funcionario func = new Funcionario();
            try
            {
                func = funcionarioBD.Buscar(id);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }

            return func;
        }

        public void excluir(int id)
        {
            try
            {
                funcionarioBD.Excluir(id);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }
        }

        public void editar(Funcionario funcionario)
        {
            try
            {
                funcionarioBD.Editar(funcionario);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }

        }

        public List<Funcionario> filtrarNome(string nome)
        {
            List<Funcionario> lista = new List<Funcionario>();
            try
            {
                lista = funcionarioBD.FiltrarNome(nome);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }
            return lista;
        }

        public List<Funcionario> filtrarCargo(string cargo)
        {
            List<Funcionario> lista = new List<Funcionario>();
            try
            {
                lista = funcionarioBD.FiltrarCargo(cargo);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }
            return lista;
        }

        public bool existeCpf(string cpf)
        {
            bool existe = false;
            try
            {
                existe = funcionarioBD.ExisteCpf(cpf);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }
            return existe;

        }
    }
}