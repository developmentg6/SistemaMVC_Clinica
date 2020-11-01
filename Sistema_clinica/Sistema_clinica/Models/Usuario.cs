using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_clinica.Models
{
    public class Usuario
    {
        [Required(ErrorMessage ="Campo não pode ficar em branco")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Senha { get; set; }

        [Display(Name = "Você é:")]
        public string Tipo { get; set; }

        public int Nivel { get; set; }

        public int Id { get; set; }

        public string erro { get; set; } = "";

        public List<SelectListItem> tipoLogin = new List<SelectListItem>() {
            new SelectListItem { Text = "Cliente", Value = "cliente" },
            new SelectListItem { Text = "Funcionario", Value = "funcionario" }
        };

        UsuarioBD usuarioBD = new UsuarioBD();

        public bool verificarLogin()
        {
            bool usuarioValido = false;
            try
            {
                usuarioValido = usuarioBD.verificarLogin(this);
            }
            catch
            {
                erro = usuarioBD.mensagem;
            }
            return usuarioValido;
        }
    }
}