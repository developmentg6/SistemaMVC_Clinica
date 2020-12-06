using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_clinica.Models;

namespace Sistema_clinica.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Funcionario funcionario = new Funcionario();
            IEnumerable<Funcionario> lista = funcionario.listaFuncionarios();
            if (funcionario.erro != "")
            {
                return View(lista).Mensagem(funcionario.erro);
            }
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Funcionario funcionario = new Funcionario();

            return View(funcionario.buscar(id));
        }

        public ActionResult Cadastrar()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }
            if (Session["nivel"].ToString() != "1")
            {
                return RedirectToAction("TelaPrincipal", "Home").Mensagem("Você não tem permissão para acessar essa página. Contate o administrador.");
            }

            Funcionario funcionario = new Funcionario();
            ViewBag.listaNivel = funcionario.listaNivel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Funcionario funcdigitado, string listaNivel)
        {
            funcdigitado.Nivel = listaNivel;
            ViewBag.listaNivel = funcdigitado.listaNivel;

            Funcionario funcCpf = new Funcionario();
            if (funcCpf.existeCpf(funcdigitado.Cpf))
            {
                ModelState.AddModelError("Cpf", "Esse cpf já está cadastrado");
            }

            Usuario usu = new Usuario();
            if (usu.existeUsuario(funcdigitado.Usuario))
            {
                ModelState.AddModelError("Usuario", "Esse usuario já está em uso. Favor escolher outro.");
            }

            Funcionario funcionario = new Funcionario();
            if (ModelState.IsValid)
            {
                funcionario.cadastrar(funcdigitado);
                if (funcionario.erro == "")
                {
                    return RedirectToAction("Index", "Funcionario").Mensagem("Funcionário cadastrado com sucesso!", "Cadastro realizado");
                }
                else
                {
                    return View(funcdigitado).Mensagem(funcionario.erro);
                }
            }
            return View(funcdigitado);
        }

        public ActionResult Excluir(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }
            if (Session["nivel"].ToString() != "1")
            {
                return RedirectToAction("TelaPrincipal", "Home").Mensagem("Você não tem permissão para acessar essa página. Contate o administrador.");
            }

            Funcionario funcionario = new Funcionario();

            return View(funcionario.buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Funcionario funcionario)
        {
            Funcionario func = new Funcionario();
            func.excluir(funcionario.IdFuncionario);
            if (func.erro == "")
            {
                return RedirectToAction("Index", "Funcionario").Mensagem("Funcionário excluído com sucesso!", "Funcionário excluído");
            }

            return View(funcionario).Mensagem("Erro ao excluir!" + func.erro, "Erro");
        }

        public ActionResult Editar(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }
            if (Session["nivel"].ToString() != "1")
            {
                return RedirectToAction("TelaPrincipal", "Home").Mensagem("Você não tem permissão para acessar essa página. Contate o administrador.");
            }

            Funcionario funcionario = new Funcionario();

            return View(funcionario.buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Funcionario funcionario)
        {

            if (ModelState.IsValid)
            {
                Funcionario func = new Funcionario();
                func.editar(funcionario);
                if (func.erro == "")
                {
                    return RedirectToAction("Index", "Funcionario").Mensagem("Funcionário alterado com sucesso!", "Funcionário alterado");
                }
                return View(funcionario).Mensagem("Erro ao alterar!" + func.erro, "Erro");
            }
            return View(funcionario);

        }

        public ActionResult FiltrarNome(string nome)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Funcionario funcionario = new Funcionario();
            IEnumerable<Funcionario> lista = funcionario.filtrarNome(nome);
            if (funcionario.erro != "")
            {
                return RedirectToAction("Index", "Funcionario").Mensagem(funcionario.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Funcionario").Mensagem("Não foi encontrado nenhum funcionário com o filtro especificado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarCargo(string cargo)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Funcionario funcionario = new Funcionario();
            IEnumerable<Funcionario> lista = funcionario.filtrarCargo(cargo);
            if (funcionario.erro != "")
            {
                return RedirectToAction("Index", "Funcionario").Mensagem(funcionario.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Funcionario").Mensagem("Não foi encontrado nenhum funcionário com o cargo especificado");
            }
            return View("Index", lista);
        }
    }
}