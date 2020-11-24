using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_clinica.Models
{
    public class ConfirmaCliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(15, MinimumLength = 14, ErrorMessage = "Preencha corretamente o CPF")]
        [DisplayFormat(DataFormatString = "{0:###.###.###-##}")]
        public string Cpf { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataBrasil(ErrorMessage = "Data inválida", DataRequerida = true)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        public string Telefone { get; set; }

        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(30, ErrorMessage = "O nome de usuário não pode ter mais de 30 caracteres")]
        public string Usuario { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(30, ErrorMessage = "A senha não pode ter mais de 30 caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Confirme a senha")]
        [Required(ErrorMessage = "Campo não pode ficar em branco")]
        [StringLength(30, ErrorMessage = "A senha não pode ter mais de 30 caracteres")]
        public string ConfirmaSenha { get; set; } = null;
        
    }
}