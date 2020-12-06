using Sistema_clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_clinica.Controllers
{
    public class AvaliacaoController : Controller
    {
        // GET: Avaliacao
        public ActionResult Index(int id = 0)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            if (Session["nivel"].ToString() == "3" && id != int.Parse(Session["id"].ToString()))
            {
                return RedirectToAction("TelaCliente", "Home");
            }

            Avaliacao avaliacao = new Avaliacao();
            IEnumerable<Avaliacao> lista = avaliacao.listaAvaliacao(id);
            if (avaliacao.erro != "")
            {
                return View(lista).Mensagem(avaliacao.erro);
            }
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            if (Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("TelaCliente", "Home");
            }

            Avaliacao avaliacao = new Avaliacao();

            return View(avaliacao.buscar(id));
        }

        public ActionResult Cadastrar()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            if (Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("TelaCliente", "Home");
            }

            Procedimento procedimento = new Procedimento();
            ViewBag.procedimentoLista = new SelectList(
                procedimento.listaProcedimentos(),
                "Id",
                "Nome"
                );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Avaliacao novaAvaliacao, string procedimentoLista)
        {
            Cliente cliente = new Cliente();
            if (!cliente.existeCpf(novaAvaliacao.Cpf_cliente))
            {
                ModelState.AddModelError("Cpf_cliente", "CPF não encontrado");
            }

            if (procedimentoLista == "")
            {
                ModelState.AddModelError("Id_procedimento", "Campo não pode ficar em branco");
            }
            else
            {
                novaAvaliacao.Id_procedimento = int.Parse(procedimentoLista);
            }
            
            Avaliacao avaliacao = new Avaliacao();
            if (ModelState.IsValid)
            {
                novaAvaliacao.buscarIdClientePeloCpf();
                avaliacao.cadastrar(novaAvaliacao);
                if (avaliacao.erro == "")
                {
                    return RedirectToAction("Index", "Avaliacao").Mensagem("Avaliação cadastrada com sucesso!", "Cadastro realizado");
                }
            }

            Procedimento procedimento = new Procedimento();
            ViewBag.procedimentoLista = new SelectList(
                procedimento.listaProcedimentos(),
                "Id",
                "Nome"
                );
            
            return View(novaAvaliacao).Mensagem(avaliacao.erro);
        }

        public ActionResult Editar(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            if (Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("TelaCliente", "Home");
            }

            Avaliacao avaliacao = new Avaliacao();
            avaliacao = avaliacao.buscar(id);

            Procedimento procedimento = new Procedimento();
            ViewBag.procedimentoLista = new SelectList(
                procedimento.listaProcedimentos(),
                "Id",
                "Nome",
                avaliacao.Id_procedimento
                );

            return View(avaliacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Avaliacao avaliacaoEdit, string procedimentoLista)
        {
            Cliente cliente = new Cliente();
            if (!cliente.existeCpf(avaliacaoEdit.Cpf_cliente))
            {
                ModelState.AddModelError("Cpf_cliente", "CPF não encontrado");
            }

            if (procedimentoLista == "")
            {
                ModelState.AddModelError("Id_procedimento", "Campo não pode ficar em branco");
            }
            else
            {
                avaliacaoEdit.Id_procedimento = int.Parse(procedimentoLista);
            }
            
            Avaliacao avaliacao = new Avaliacao();
            if (ModelState.IsValid)
            {
                avaliacaoEdit.buscarIdClientePeloCpf();
                avaliacao.editar(avaliacaoEdit);
                if (avaliacao.erro == "")
                {
                    return RedirectToAction("Index", "Avaliacao").Mensagem("Avaliação alterada com sucesso!", "Alteração realizada");
                }
            }

            Procedimento procedimento = new Procedimento();
            ViewBag.procedimentoLista = new SelectList(
                procedimento.listaProcedimentos(),
                "Id",
                "Nome",
                avaliacaoEdit.Id_procedimento
                );
            
            return View(avaliacaoEdit).Mensagem(avaliacao.erro);

        }


        public ActionResult Excluir(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            if (Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("TelaCliente", "Home");
            }

            Avaliacao avaliacao = new Avaliacao();

            return View(avaliacao.buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Avaliacao avaliacao)
        {
            Avaliacao aval = new Avaliacao();
            aval.excluir(avaliacao.Id);
            if (aval.erro == "")
            {
                return RedirectToAction("Index", "Avaliacao").Mensagem("Avaliação excluída com sucesso!", "Avaliação excluída");
            }

            return View(avaliacao).Mensagem("Erro ao excluir!" + aval.erro, "Erro");
        }


        public ActionResult FiltrarNomeCliente(string nome)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            if (Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("TelaCliente", "Home");
            }

            Avaliacao avaliacao = new Avaliacao();
            IEnumerable<Avaliacao> lista = avaliacao.filtrarNomeCliente(nome);
            if (avaliacao.erro != "")
            {
                return RedirectToAction("Index", "Avaliacao").Mensagem(avaliacao.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Avaliacao").Mensagem("Não foi encontrado nenhum cliente com o nome informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarCpfCliente(string cpf)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            if (Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("TelaCliente", "Home");
            }

            Avaliacao avaliacao = new Avaliacao();
            IEnumerable<Avaliacao> lista = avaliacao.filtrarCpfCliente(cpf);
            if (avaliacao.erro != "")
            {
                return RedirectToAction("Index", "Avaliacao").Mensagem(avaliacao.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Avaliacao").Mensagem("Não foi encontrado nenhum cliente com o cpf informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarProcedimento(string nomeProcedimento)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            if (Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("TelaCliente", "Home");
            }

            Avaliacao avaliacao = new Avaliacao();
            IEnumerable<Avaliacao> lista = avaliacao.filtrarProcedimento(nomeProcedimento);
            if (avaliacao.erro != "")
            {
                return RedirectToAction("Index", "Avaliacao").Mensagem(avaliacao.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Avaliacao").Mensagem("Não foi encontrado nenhum procedimento com o nome informado");
            }
            return View("Index", lista);
        }

    }
}