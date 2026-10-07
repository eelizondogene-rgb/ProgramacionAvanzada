using Microsoft.AspNetCore.Mvc;
using GestorClientes.Application.Services;

namespace GestorClientes.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteController(IClienteServicio clienteServicio) // Inyección de dependencias
        {
            _clienteServicio = clienteServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        // TODO (Persona 3): agregar las acciones que devuelven JSON:
        // ObtenerClientes, ObtenerClientePorId, CrearCliente, ActualizarCliente y EliminarCliente.
    }
}
