using Sistema_clinica.Models.bd;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models
{
    public class Usuario
    {
        [Required(ErrorMessage ="Campo não pode ficar em branco")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Senha { get; set; }

        public string erro { get; set; } = "";

        public static string usuarioLogado { get; set; } = "";

        UsuarioBD usuarioBD = new UsuarioBD();

        public bool verificarLogin()
        {
            bool usuarioValido = false;
            try
            {
                usuarioValido = usuarioBD.verificarLogin(this.Login, this.Senha);
                if (usuarioValido)
                {
                    usuarioLogado = this.Login;
                }
            }
            catch
            {
                erro = usuarioBD.mensagem;
            }
            return usuarioValido;
        }
    }
}