using Microsoft.AspNetCore.Mvc;
using GestorClientes.Application.Services;
using GestorClientes.Application.Dtos;
using System.Threading;

namespace GestorClientes.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : Controller
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteController(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        // MVC - GET /Cliente/Index (fuera de la ruta API)
        [HttpGet("/Cliente/Index")]
        public IActionResult Index()
        {
            return View();
        }

        // API - GET /api/clientes
        [HttpGet]
        public async Task<IActionResult> ObtenerClientes(CancellationToken cancellationToken = default)
        {
            var clientes = await _clienteServicio.ObtenerClientesAsync(cancellationToken);
            return Ok(new { EsCorrecto = true, Codigo = 200, Mensaje = "Operación realizada correctamente", Dato = clientes });
        }

        // API - GET /api/clientes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerClientePorId(int id, CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteServicio.ObtenerClientePorIdAsync(id, cancellationToken);

            if (cliente == null)
                return NotFound();

            return Ok(new { EsCorrecto = true, Codigo = 200, Mensaje = "Operación realizada correctamente", Dato = cliente });
        }

        // API - POST /api/clientes
        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteDto cliente, CancellationToken cancellationToken = default)
        {
            var respuesta = await _clienteServicio.CrearClienteAsync(cliente, cancellationToken);

            if (!respuesta.EsCorrecto)
                return StatusCode(respuesta.Codigo, respuesta);

            // Éxito: establecer código 201 y devolver Created
            respuesta.Codigo = 201;
            return StatusCode(201, respuesta);
        }

        // API - PUT /api/clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteDto cliente, CancellationToken cancellationToken = default)
        {
            // Verificar que el ID de la ruta coincida con el ID del cuerpo
            if (id != cliente.Id)
                return BadRequest(new Respuesta<ClienteDto>
                {
                    EsCorrecto = false,
                    Mensaje = "El ID de la ruta no coincide con el ID del cliente.",
                    Codigo = 400
                });

            var respuesta = await _clienteServicio.ActualizarClienteAsync(cliente, cancellationToken);

            if (!respuesta.EsCorrecto)
                return StatusCode(respuesta.Codigo, respuesta);

            return Ok(respuesta);
        }

        // API - DELETE /api/clientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id, CancellationToken cancellationToken = default)
        {
            var respuesta = await _clienteServicio.EliminarClienteAsync(id, cancellationToken);

            if (!respuesta.EsCorrecto)
                return StatusCode(respuesta.Codigo, respuesta);

            return Ok(respuesta);
        }
    }
}
