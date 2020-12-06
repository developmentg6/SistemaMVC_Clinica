using Sistema_clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_clinica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contato contato)
        {
            new Contato().enviarContato(contato);
            if (contato.Erro == "")
            {
                return View().Mensagem("Mensagem enviada com sucesso!");
            }
            else
            {
                return View().Mensagem(contato.Erro);
            }
        }


        public ActionResult Servicos()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.tipoLogin = new Usuario().tipoLogin;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            usuario.Tipo = Request["tipoLogin"];
            ViewBag.tipoLogin = new Usuario().tipoLogin;

            if (ModelState.IsValid) {
                bool usuarioValido = usuario.verificarLogin();
                if (usuarioValido)
                {
                    Session.Add("usuario", usuario.Login);
                    Session.Add("nivel", usuario.Nivel);
                    Session.Add("id", usuario.Id);

                    if (Session["nivel"].ToString() == "3")
                    {
                        return RedirectToAction("TelaCliente", "Home");
                    }
                    else
                    {
                        return RedirectToAction("TelaPrincipal", "Home");
                    }
                }
                else if(usuario.erro != ""){
                    return View(usuario).Mensagem("Erro com banco de dados! " + usuario.erro);
                }
                else
                {
                    return View(usuario).Mensagem("Usuário e/ou senha incorretos", "Erro");
                }
            }
            return View();
        }

        public ActionResult Deslogar()
        {
            Session.Clear();
            return RedirectToAction("Login").Mensagem("Usuário deslogado com sucesso!", "Sessão encerrada");
        }

        public ActionResult TelaPrincipal()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "" || Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }
            return View();
        }

        public ActionResult TelaCliente()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "" || Session["nivel"].ToString() != "3")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }
            return View();
        }

        public ActionResult AlterarSenha()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            var usuario = new Usuario();
            usuario.Id = int.Parse(Session["id"].ToString());

            return View(usuario);
        }

        [HttpPost]
        public ActionResult AlterarSenha(Usuario usuario)
        {
            if (usuario.Login != Session["usuario"].ToString())
            {
                ModelState.AddModelError("Login", "Digite o usuário correto");
            }

            if (usuario.NovaSenha != usuario.ConfirmaSenha)
            {
                ModelState.AddModelError("ConfirmaSenha", "A confirmação está diferente da nova senha");
            }

            usuario.Tipo = Session["nivel"].ToString() == "3" ? "cliente" : "funcionario";
            bool usuarioValido = usuario.verificarLogin();
            if (!usuarioValido)
            {
                ModelState.AddModelError("Senha", "Usuário e/ou senha incorretos!");
            }

            if (ModelState.IsValid)
            {
                var usu = new Usuario();
                usu.atualizarSenha(usuario);

                var nivel = Session["nivel"].ToString() == "3" ? "TelaCliente" : "TelaPrincipal";
                return View(nivel).Mensagem("Senha alterada com sucesso!");
            }
            else
            {
                return View(usuario);
            }
        }
    }
}