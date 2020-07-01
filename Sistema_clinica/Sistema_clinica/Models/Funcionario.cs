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
        [StringLength(50, ErrorMessage = "O Nome não pode ter mais de 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(50, ErrorMessage = "O Cargo não pode ter mais de 50 caracteres")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(11, ErrorMessage = "O Telefone não pode ter mais de 11 dígitos")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(11, ErrorMessage = "O CPF não pode ter mais de 11 dígitos")]
        public string Cpf { get; set; }

        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido")]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Endereco { get; set; }

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