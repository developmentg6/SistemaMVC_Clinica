﻿using Sistema_clinica.Models.bd;
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
        [StringLength(50, ErrorMessage = "O Nome não pode ter mais de 50 caracteres")]
        public string Nome { get; set; }

        public string Sexo { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Cpf { get; set; }

        [Display(Name ="Data de Nascimento")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Endereco { get; set; }

        public string Email { get; set; }

        [Display(Name = "Profissão")]
        public string Profissao { get; set; }

        [Display(Name = "Histórico")]
        public string Historico { get; set; }

        ClienteBD clienteBD = new ClienteBD();
        public string erro { get; set; } = "";


        public List<Cliente> ListaClientes()
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

        public void Cadastrar(Cliente cliente)
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

        public Cliente Buscar(int id)
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

        public void Excluir(int id)
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

        public void Editar(Cliente cliente)
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
    }
}