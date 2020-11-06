﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_clinica.Models;
using Sistema_clinica.Models.bd;


namespace Sistema_clinica.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Cliente cliente = new Cliente();
            IEnumerable<Cliente> lista = cliente.listaClientes();
            if (cliente.erro != "")
            {
                return View(lista).Mensagem(cliente.erro);
            }
            return View(lista);
        }

        public ActionResult detalhes(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Cliente cliente = new Cliente();

            return View(cliente.buscar(id));
        }

        public ActionResult Cadastrar()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Cliente cliente = new Cliente();
            ViewBag.listaSexo = cliente.listaSexo;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Cliente clientedigitado, string listaSexo)
        {
            clientedigitado.Sexo = listaSexo;
            Cliente cliCpf = new Cliente();
            if (cliCpf.existeCpf(clientedigitado.Cpf))
            {
                ModelState.AddModelError("Cpf", "Esse cpf já está cadastrado");
            }

            Cliente cliente = new Cliente();
            ViewBag.listaSexo = cliente.listaSexo;
            if (ModelState.IsValid)
            {
                cliente.cadastrar(clientedigitado);
                if (cliente.erro == "")
                {
                    return RedirectToAction("Index", "Cliente").Mensagem("Cliente cadastrado com sucesso!", "Cadastro realizado");
                }
                else
                {
                    return View(clientedigitado).Mensagem(cliente.erro);
                }
            }
            return View(clientedigitado);
        }

        public ActionResult Excluir(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Cliente cliente = new Cliente();

            return View(cliente.buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Cliente cliente)
        {
            Cliente cli = new Cliente();
            cli.excluir(cliente.Id);
            if (cli.erro == "")
            {
                return RedirectToAction("Index", "Cliente").Mensagem("Cliente excluído com sucesso!", "Cliente excluído");
            }

            return View(cliente).Mensagem("Erro ao excluir!" + cli.erro, "Erro");
        }

        public ActionResult Editar(int id)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Cliente cliente = new Cliente();
            cliente = cliente.buscar(id);

            ViewBag.listaSexo = new SelectList(
                cliente.listaSexo,
                "Value",
                "Text",
                cliente.Sexo
                );

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cliente cliente, string listaSexo)
        {
            cliente.Sexo = listaSexo;

            Cliente cliSexo = new Cliente();
            ViewBag.listaSexo = cliSexo.listaSexo;

            if (ModelState.IsValid)
            {
                Cliente cli = new Cliente();
                cli.editar(cliente);
                if (cli.erro == "")
                {
                    return RedirectToAction("Index", "Cliente").Mensagem("Cliente alterado com sucesso!", "Cliente alterado");
                }
                return View(cliente).Mensagem("Erro ao alterar!" + cli.erro, "Erro");
            }
            return View(cliente);

        }

        public ActionResult FiltrarNome(string nome)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Cliente cliente = new Cliente();
            IEnumerable<Cliente> lista = cliente.filtrarNome(nome);
            if (cliente.erro != "")
            {
                return RedirectToAction("Index", "Cliente").Mensagem(cliente.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Cliente").Mensagem("Não foi encontrado nenhum cliente com o filtro especificado");
            }
            return View("Index", lista);
        }

        public ActionResult FiltrarCpf(string cpf)
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Cliente cliente = new Cliente();
            IEnumerable<Cliente> lista = cliente.filtrarCpf(cpf);
            if (cliente.erro != "")
            {
                return RedirectToAction("Index", "Cliente").Mensagem(cliente.erro);
            }
            if (lista.Count() == 0)
            {
                return RedirectToAction("Index", "Cliente").Mensagem("Não foi encontrado nenhum cliente com o filtro especificado");
            }
            return View("Index", lista);
        }

        public ActionResult Relatorio()
        {
            if (Session["usuario"] == null || Session["usuario"].ToString() == "" || Session["nivel"].ToString() == "3")
            {
                return RedirectToAction("Login", "Home").Mensagem("Faça o login para entrar");
            }

            Cliente cliente = new Cliente();
            IEnumerable<Cliente> lista = cliente.listaClientes();
            if (cliente.erro != "")
            {
                return View("Tela Principal", "Home").Mensagem(cliente.erro);
            }
            var excel = lista.ToList();

            Response.AddHeader("content-disposition", "attachment; filename=relClientes.xls");
            Response.ContentType = "application/vnd.ms-excel";
            return View(excel);
        }

    }
}