using Sistema_clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_clinica.Controllers
{
    public class SessaoController : Controller
    {

        // GET: Sessao
        public ActionResult Index()
        {
            Sessao sessao = new Sessao();
            IEnumerable<Sessao> lista = sessao.listaSessao();
            if (sessao.erro != "")
            {
                return View(lista).Mensagem(sessao.erro);
            }
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            Sessao sessao = new Sessao();

            return View(sessao.buscar(id));
        }

        public ActionResult Cadastrar()
        {
            Procedimento procedimento = new Procedimento();
            ViewBag.procedimentoLista = new SelectList(
                procedimento.listaProcedimentos(),
                "Id",
                "Nome"
                );

            Funcionario funcionario = new Funcionario();
            ViewBag.funcionarioLista = new SelectList(
                funcionario.listaFuncionarios(),
                "IdFuncionario",
                "Nome"
                );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Sessao novaSessao, string procedimentoLista, string funcionarioLista)
        {
            Cliente cliente = new Cliente();
            if (!cliente.existeCpf(novaSessao.Cpf_cliente))
            {
                ModelState.AddModelError("Cpf_cliente", "CPF não encontrado");
            }

            if (procedimentoLista == "")
            {
                ModelState.AddModelError("Id_procedimento", "Campo não pode ficar em branco");
            }
            else
            {
                novaSessao.Id_procedimento = int.Parse(procedimentoLista);
            }

            if (funcionarioLista == "")
            {
                ModelState.AddModelError("Id_funcionario", "Campo não pode ficar em branco");
            }
            else
            {
                novaSessao.Id_funcionario = int.Parse(funcionarioLista);
            }

            Sessao sessao = new Sessao();
            if (ModelState.IsValid)
            {
                novaSessao.buscarIdClientePeloCpf();
                sessao.cadastrar(novaSessao);
                if (sessao.erro == "")
                {
                    return RedirectToAction("Index", "Sessao").Mensagem("Sessão cadastrada com sucesso!", "Cadastro realizado");
                }
            }

            Procedimento procedimento = new Procedimento();
            ViewBag.procedimentoLista = new SelectList(
                procedimento.listaProcedimentos(),
                "Id",
                "Nome"
                );

            Funcionario funcionario = new Funcionario();
            ViewBag.funcionarioLista = new SelectList(
                funcionario.listaFuncionarios(),
                "IdFuncionario",
                "Nome"
                );

            return View(novaSessao).Mensagem(sessao.erro);
        }

        public ActionResult Editar(int id)
        {
            Sessao sessao = new Sessao();
            sessao = sessao.buscar(id);

            Procedimento procedimento = new Procedimento();
            ViewBag.procedimentoLista = new SelectList(
                procedimento.listaProcedimentos(),
                "Id",
                "Nome",
                sessao.Id_procedimento
                );

            Funcionario funcionario = new Funcionario();
            ViewBag.funcionarioLista = new SelectList(
                funcionario.listaFuncionarios(),
                "IdFuncionario",
                "Nome",
                sessao.Id_funcionario
                );

            return View(sessao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Sessao sessaoEdit, string procedimentoLista, string funcionarioLista)
        {
            Cliente cliente = new Cliente();
            if (!cliente.existeCpf(sessaoEdit.Cpf_cliente))
            {
                ModelState.AddModelError("Cpf_cliente", "CPF não encontrado");
            }

            if (procedimentoLista == "")
            {
                ModelState.AddModelError("Id_procedimento", "Campo não pode ficar em branco");
            }
            else
            {
                sessaoEdit.Id_procedimento = int.Parse(procedimentoLista);
            }

            if (funcionarioLista == "")
            {
                ModelState.AddModelError("Id_funcionario", "Campo não pode ficar em branco");
            }
            else
            {
                sessaoEdit.Id_funcionario = int.Parse(funcionarioLista);
            }

            Sessao sessao = new Sessao();
            if (ModelState.IsValid)
            {
                sessaoEdit.buscarIdClientePeloCpf();
                sessao.editar(sessaoEdit);
                if (sessao.erro == "")
                {
                    return RedirectToAction("Index", "Sessao").Mensagem("Sessão alterada com sucesso!", "Alteração realizada");
                }
            }

            Procedimento procedimento = new Procedimento();
            ViewBag.procedimentoLista = new SelectList(
                procedimento.listaProcedimentos(),
                "Id",
                "Nome",
                sessao.Id_procedimento
                );

            Funcionario funcionario = new Funcionario();
            ViewBag.funcionarioLista = new SelectList(
                funcionario.listaFuncionarios(),
                "IdFuncionario",
                "Nome",
                sessao.Id_funcionario
                );

            return View(sessaoEdit).Mensagem(sessao.erro);

        }


        public ActionResult Excluir(int id)
        {
            Sessao sessao = new Sessao();

            return View(sessao.buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Sessao sessao)
        {
            Sessao ses = new Sessao();
            ses.excluir(sessao.Id);
            if (ses.erro == "")
            {
                return RedirectToAction("Index", "Sessao").Mensagem("Sessão excluída com sucesso!", "Sessão excluída");
            }

            return View(sessao).Mensagem("Erro ao excluir!" + ses.erro, "Erro");
        }


        public ActionResult FiltrarNomeCliente(string nome)
        {
            Sessao sessao = new Sessao();
            IEnumerable<Sessao> lista = sessao.filtrarNomeCliente(nome);
            if (sessao.erro != "")
            {
                return RedirectToAction("Index", "Sessao").Mensagem(sessao.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Sessao").Mensagem("Não foi encontrado nenhum cliente com o nome informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarCpfCliente(string cpf)
        {
            Sessao sessao = new Sessao();
            IEnumerable<Sessao> lista = sessao.filtrarCpfCliente(cpf);
            if (sessao.erro != "")
            {
                return RedirectToAction("Index", "Sessao").Mensagem(sessao.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Sessao").Mensagem("Não foi encontrado nenhum cliente com o cpf informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarProcedimento(string nomeProcedimento)
        {
            Sessao sessao = new Sessao();
            IEnumerable<Sessao> lista = sessao.filtrarProcedimento(nomeProcedimento);
            if (sessao.erro != "")
            {
                return RedirectToAction("Index", "Sessao").Mensagem(sessao.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Sessao").Mensagem("Não foi encontrado nenhum cliente com o nome informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarFuncionario(string nomeFuncionario)
        {
            Sessao sessao = new Sessao();
            IEnumerable<Sessao> lista = sessao.filtrarFuncionario(nomeFuncionario);
            if (sessao.erro != "")
            {
                return RedirectToAction("Index", "Sessao").Mensagem(sessao.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Sessao").Mensagem("Não foi encontrado nenhum cliente com o nome informado");
            }
            return View("Index", lista);
        }

    }
}