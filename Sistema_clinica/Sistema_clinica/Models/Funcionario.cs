using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [StringLength(15, MinimumLength = 14, ErrorMessage = "Preencha corretamente o CPF")]
        public string Cpf { get; set; }

        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido")]
        [StringLength(60, ErrorMessage = "O Email não pode ter mais de 60 caracteres")]
        public string Email { get; set; }

        [StringLength(30, ErrorMessage = "O Nome de Usuário não pode ter mais de 30 caracteres")]
        public string Usuario { get; set; }

        [StringLength(30, ErrorMessage = "A Senha não pode ter mais de 30 caracteres")]
        public string Senha { get; set; }

        public string Nivel { get; set; }

        FuncionarioBD funcionarioBD = new FuncionarioBD();
        public string erro { get; set; } = "";

        public List<SelectListItem> listaNivel = new List<SelectListItem>() {
            new SelectListItem { Text = "Funcionário", Value = "2" },
            new SelectListItem { Text = "Administrador", Value = "1" }
        };


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
            funcionario.Cpf = funcionario.Cpf.Remove(11, 1).Remove(7, 1).Remove(3, 1);
            funcionario.Telefone = funcionario.Telefone.Remove(9, 1).Remove(3, 1).Remove(0, 1);
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
            funcionario.Cpf = funcionario.Cpf.Remove(11, 1).Remove(7, 1).Remove(3, 1);
            funcionario.Telefone = funcionario.Telefone.Remove(9, 1).Remove(3, 1).Remove(0, 1);
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
            if (cpf == null || cpf.Length != 14){
                return false;
            }
            string cpfEditado = cpf.Remove(11, 1).Remove(7, 1).Remove(3, 1);

            bool existe = false;
            try
            {
                existe = funcionarioBD.ExisteCpf(cpfEditado);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }
            return existe;

        }

        public bool existeUsuario(string usuario)
        {
            bool existe = false;
            try
            {
                existe = funcionarioBD.ExisteUsuario(usuario);
            }
            catch
            {
                erro = funcionarioBD.mensagem;
            }
            return existe;

        }
    }
}