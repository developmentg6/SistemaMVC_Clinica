using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(60, ErrorMessage = "O Nome não pode ter mais de 60 caracteres")]
        public string Nome { get; set; }

        public string Sexo { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Cpf { get; set; }

        [Display(Name ="Data de Nascimento")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        public DateTime DataNascimento { get; set; }

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

        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido")]
        [StringLength(60, ErrorMessage = "O Email não pode ter mais de 60 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Telefone { get; set; }

        [Display(Name = "Profissão")]
        [StringLength(30, ErrorMessage = "A Profissão não pode ter mais de 30 caracteres")]
        public string Profissao { get; set; }

        [Display(Name = "Histórico")]
        [StringLength(500, ErrorMessage = "O Histórico não pode ter mais de 500 caracteres")]
        public string Historico { get; set; }

        ClienteBD clienteBD = new ClienteBD();
        public string erro { get; set; } = "";


        public List<Cliente> listaClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                lista = clienteBD.ListaClientes();
            }
            catch
            {
                erro = clienteBD.mensagem;
            }
            return lista;
        }

        public void cadastrar(Cliente cliente)
        {
            try
            {
                clienteBD.Cadastrar(cliente);
            }
            catch
            {
                erro = clienteBD.mensagem;
            }

        }

        public Cliente buscar(int id)
        {
            Cliente cliente = new Cliente();
            try
            {
                cliente = clienteBD.Buscar(id);
            }
            catch
            {
                erro = clienteBD.mensagem;
            }

            return cliente;
        }

        public void excluir(int id)
        {
            try
            {
                clienteBD.Excluir(id);
            }
            catch
            {
                erro = clienteBD.mensagem;
            }
        }

        public void editar(Cliente cliente)
        {
            try
            {
                clienteBD.Editar(cliente);
            }
            catch
            {
                erro = clienteBD.mensagem;
            }

        }

        public List<Cliente> filtrarNome(string nome)
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                lista = clienteBD.FiltrarNome(nome);
            }
            catch
            {
                erro = clienteBD.mensagem;
            }
            return lista;
        }

        public List<Cliente> filtrarCpf(string cpf)
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                lista = clienteBD.FiltrarCpf(cpf);
            }
            catch
            {
                erro = clienteBD.mensagem;
            }
            return lista;
        }

        public bool existeCpf(string cpf)
        {
            bool existe = false;
            try{
                existe = clienteBD.ExisteCpf(cpf);
            }
            catch
            {
                erro = clienteBD.mensagem;
            }
            return existe;

        }

    }
}