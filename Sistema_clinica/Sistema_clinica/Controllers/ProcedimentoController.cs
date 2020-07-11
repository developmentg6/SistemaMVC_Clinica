using Sistema_clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_clinica.Controllers
{
    public class ProcedimentoController : Controller
    {
        // GET: Procedimento
        public ActionResult Index()
        {
            Procedimento procedimento = new Procedimento();
            IEnumerable<Procedimento> lista = procedimento.listaProcedimentos();
            if (procedimento.erro != "")
            {
                return View(lista).Mensagem(procedimento.erro);
            }
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            Procedimento procedimento = new Procedimento();

            return View(procedimento.buscar(id));
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Procedimento procdigitado)
        {
            Procedimento procNome = new Procedimento();
            if (procNome.existeNome(procdigitado.Nome))
            {
                ModelState.AddModelError("Nome", "Esse nome já está cadastrado");
            }

            Procedimento procedimento = new Procedimento();
            if (ModelState.IsValid)
            {
                procedimento.cadastrar(procdigitado);
                if (procedimento.erro == "")
                {
                    return RedirectToAction("Index", "Procedimento").Mensagem("Procedimento cadastrado com sucesso!", "Cadastro realizado");
                }
                else
                {
                    return View(procdigitado).Mensagem(procedimento.erro);
                }
            }
            return View(procdigitado);
        }

        public ActionResult Excluir(int id)
        {
            Procedimento procedimento = new Procedimento();

            return View(procedimento.buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Procedimento procedimento)
        {
            Procedimento proc = new Procedimento();
            proc.excluir(procedimento.Id);
            if (proc.erro == "")
            {
                return RedirectToAction("Index", "Procedimento").Mensagem("Procedimento excluído com sucesso!", "Procedimento excluído");
            }

            return View(procedimento).Mensagem("Erro ao excluir! " + proc.erro, "Erro");
        }

        public ActionResult Editar(int id)
        {
            Procedimento procedimento = new Procedimento();

            return View(procedimento.buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Procedimento procedimento)
        {

            if (ModelState.IsValid)
            {
                Procedimento proc = new Procedimento();
                proc.editar(procedimento);
                if (proc.erro == "")
                {
                    return RedirectToAction("Index", "Procedimento").Mensagem("Procedimento alterado com sucesso!", "Procedimento alterado");
                }
                return View(procedimento).Mensagem("Erro ao alterar! " + proc.erro, "Erro");
            }
            return View(procedimento);

        }

        public ActionResult FiltrarNome(string nome)
        {
            Procedimento procedimento = new Procedimento();
            IEnumerable<Procedimento> lista = procedimento.filtrarNome(nome);
            if (procedimento.erro != "")
            {
                return RedirectToAction("Index", "Procedimento").Mensagem(procedimento.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Procedimento").Mensagem("Não foi encontrado nenhum procedimento com o nome informado");
            }
            return View("Index", lista);
        }
    }
}