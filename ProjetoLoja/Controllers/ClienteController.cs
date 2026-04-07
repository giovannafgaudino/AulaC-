using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Interfaces;
using ProjetoLoja.Models;

namespace ProjetoLoja.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteRepositorio _clienteRepositorio; 

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public IActionResult Index() 
        {
            var clientes = _clienteRepositorio.ListarTudo();
            return View(clientes); 
        }
        [HttpGet]

        public IActionResult Cadastrar() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ClienteViewModel cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            _clienteRepositorio.CadastrarCliente(cliente);
            return RedirectToAction("Index");
        }
    }
}
