using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

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

        public string Erro { get; set; } = "";

        public void enviarContato(Contato contato)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("clinicaniceface@gmail.com");
                message.To.Add(new MailAddress("clinicaniceface@gmail.com"));
                message.Subject = "Contato do site";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "<p>From: " + contato.Nome + "</p> <p>Email: " + contato.Email + "</p> <p>Mensagem: " + contato.Mensagem + "</p>";

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("clinicaniceface@gmail.com", "SenhadoEmail");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ep)
            {
                contato.Erro = "Erro ao enviar. " + ep.Message;
            }
        }
    
    }
}