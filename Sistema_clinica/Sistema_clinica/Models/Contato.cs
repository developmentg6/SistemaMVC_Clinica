using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models
{
    public class Contato
    {
        [Required(ErrorMessage ="Campo não pode ficar em branco")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Mensagem { get; set; }
    }
}