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
    }
}