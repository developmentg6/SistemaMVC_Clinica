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
            Funcionario funcionario = new Funcionario();

            return View(funcionario.buscar(id));
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Funcionario funcdigitado)
        {
            Funcionario funcionario = new Funcionario();
            if (funcionario.existeCpf(funcdigitado.Cpf))
            {
                ModelState.AddModelError("Cpf", "Esse cpf já está cadastrado");
            }
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
            Funcionario funcionario = new Funcionario();
            IEnumerable<Funcionario> lista = funcionario.filtrarNome(nome);
            if (funcionario.erro != "")
            {
                return RedirectToAction("Index", "Funcionario").Mensagem(funcionario.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Funcionário").Mensagem("Não foi encontrado nenhum funcionário com o filtro especificado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarCpf(string cpf)
        {
            Funcionario funcionario = new Funcionario();
            IEnumerable<Funcionario> lista = funcionario.filtrarCpf(cpf);
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
    }
}