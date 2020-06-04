using System;
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
            Cliente cliente = new Cliente();

            return View(cliente.ListaClientes());
        }

        public ActionResult detalhes(int id)
        {
            Cliente cliente = new Cliente();

            return View(cliente.Buscar(id));
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Cliente clientedigitado)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente();
                cliente.Cadastrar(clientedigitado);
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
            Cliente cliente = new Cliente();

            return View(cliente.Buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Cliente cliente)
        {
            Cliente cli = new Cliente();
            cli.Excluir(cliente.Id);
            if (cli.erro == "")
            {
                return RedirectToAction("Index", "Cliente").Mensagem("Cliente excluído com sucesso!", "Cliente excluído");
            }

            return View(cliente).Mensagem("Erro ao excluir!" + cli.erro, "Erro");
        }

        public ActionResult Editar(int id)
        {
            Cliente cliente = new Cliente();

            return View(cliente.Buscar(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                Cliente cli = new Cliente();
                cli.Editar(cliente);
                if (cli.erro == "")
                {
                    return RedirectToAction("Index", "Cliente").Mensagem("Cliente alterado com sucesso!", "Cliente alterado");
                }
                return View(cliente).Mensagem("Erro ao alterar!" + cli.erro, "Erro");
            }
            return View(cliente);

        }
    }
}