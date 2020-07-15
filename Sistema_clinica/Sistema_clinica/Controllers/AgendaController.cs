using Sistema_clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_clinica.Controllers
{
    public class AgendaController : Controller
    {
        // GET: Agenda
        public ActionResult Index()
        {
            Agenda agenda = new Agenda();
            IEnumerable<Agenda> lista = agenda.listaAgenda();
            if (agenda.erro != "")
            {
                return View(lista).Mensagem(agenda.erro);
            }
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            Agenda agenda = new Agenda();

            return View(agenda.buscar(id));
        }

        public ActionResult Cadastrar(int id_sessao)
        {
            Agenda agenda = new Agenda();
            agenda.preencherAgendaComSessao(id_sessao);
            ViewBag.listaEstado = agenda.listaEstado;
            ViewBag.listaPagamento = agenda.listaPagamento;

            return View(agenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Agenda novaAgenda, string listaEstado, string listaPagamento)
        {
            novaAgenda.Estado = listaEstado;
            novaAgenda.Pago = char.Parse(listaPagamento);

            Agenda agenda = new Agenda();
            if (ModelState.IsValid)
            {
                agenda.cadastrar(novaAgenda);
                if (agenda.erro == "")
                {
                    return RedirectToAction("Index", "Agenda").Mensagem("Agendamento cadastrado com sucesso!", "Cadastro realizado");
                }
            }

            ViewBag.listaEstado = agenda.listaEstado;
            ViewBag.listaPagamento = agenda.listaPagamento;

            return View(novaAgenda).Mensagem(agenda.erro);
        }


        public ActionResult Editar(int id)
        {
            Agenda agenda = new Agenda();
            agenda = agenda.buscar(id);

            ViewBag.listaEstado = new SelectList(
                agenda.listaEstado,
                "Value",
                "Text",
                agenda.Estado
                );

            ViewBag.listaPagamento = new SelectList(
                agenda.listaPagamento,
                "Value",
                "Text",
                agenda.Pago
                );

            return View(agenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Agenda agendaEdit, string listaEstado, string listaPagamento)
        {
            agendaEdit.Estado = listaEstado;
            agendaEdit.Pago = char.Parse(listaPagamento);

            Agenda agenda = new Agenda();
            if (ModelState.IsValid)
            {
                agenda.editar(agendaEdit);
                if (agenda.erro == "")
                {
                    return RedirectToAction("Index", "Agenda").Mensagem("Agendamento alterado com sucesso!", "Alteração realizada");
                }
            }

            ViewBag.listaEstado = new SelectList(
                agenda.listaEstado,
                "Value",
                "Text",
                agenda.Estado
                );

            ViewBag.listaPagamento = new SelectList(
                agenda.listaPagamento,
                "Value",
                "Text",
                agenda.Pago
                );

            return View(agendaEdit).Mensagem(agenda.erro);

        }

        public ActionResult Excluir(int id)
        {
            Agenda agenda = new Agenda();

            return View(agenda.buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Agenda agendaExcluir)
        {
            Agenda agenda = new Agenda();
            agenda.excluir(agendaExcluir.Id);
            if (agenda.erro == "")
            {
                return RedirectToAction("Index", "Agenda").Mensagem("Agendamento excluído com sucesso!", "Agenda excluída");
            }

            return View(agendaExcluir).Mensagem("Erro ao excluir!" + agenda.erro, "Erro");
        }

        public ActionResult FiltrarNomeCliente(string nome)
        {
            Agenda agenda = new Agenda();
            IEnumerable<Agenda> lista = agenda.filtrarNomeCliente(nome);
            if (agenda.erro != "")
            {
                return RedirectToAction("Index", "Agenda").Mensagem(agenda.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Agenda").Mensagem("Não foi encontrado nenhum cliente com o nome informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarCpfCliente(string cpf)
        {
            Agenda agenda = new Agenda();
            IEnumerable<Agenda> lista = agenda.filtrarCpfCliente(cpf);
            if (agenda.erro != "")
            {
                return RedirectToAction("Index", "Agenda").Mensagem(agenda.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Agenda").Mensagem("Não foi encontrado nenhum cliente com o cpf informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarProcedimento(string nomeProcedimento)
        {
            Agenda agenda = new Agenda();
            IEnumerable<Agenda> lista = agenda.filtrarProcedimento(nomeProcedimento);
            if (agenda.erro != "")
            {
                return RedirectToAction("Index", "Agenda").Mensagem(agenda.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Agenda").Mensagem("Não foi encontrado nenhum procedimento com o nome informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarFuncionario(string nomeFuncionario)
        {
            Agenda agenda = new Agenda();
            IEnumerable<Agenda> lista = agenda.filtrarFuncionario(nomeFuncionario);
            if (agenda.erro != "")
            {
                return RedirectToAction("Index", "Agenda").Mensagem(agenda.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Agenda").Mensagem("Não foi encontrado nenhum funcionário com o nome informado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarEstado(string estado)
        {
            Agenda agenda = new Agenda();
            IEnumerable<Agenda> lista = agenda.filtrarEstado(estado);
            if (agenda.erro != "")
            {
                return RedirectToAction("Index", "Agenda").Mensagem(agenda.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Agenda").Mensagem("Não foi encontrado o estado informado");
            }
            return View("Index", lista);
        }

        public ActionResult Relatorio()
        {
            Agenda agenda = new Agenda();
            IEnumerable<Agenda> lista = agenda.listaAgenda();
            if (agenda.erro != "")
            {
                //return View(lista).Mensagem(agenda.erro);
            }
            //return View(lista);
                var excel = lista.ToList();//Sua lista que está enviando apra View.

                Response.AddHeader("content-disposition", "attachment; filename=relatorio.xls");
                Response.ContentType = "application/vnd.ms-excel";
                return View(excel);
        }

    }
}