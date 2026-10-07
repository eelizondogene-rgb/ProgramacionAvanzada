using AutoMapper;
using GestorClientes.Application.Dtos;
using GestorClientes.Infrastructure.Repository;

namespace GestorClientes.Application.Services
{
    // Principio SOLID de inversión de dependencias: se depende de la interfaz IClienteRepository,
    // no de la clase concreta.
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteServicio(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        // TODO (Persona 2): implementar los 5 métodos. Validar las reglas de negocio antes de
        // guardar y devolver un Respuesta<ClienteDto> con el resultado.

        public Task<List<ClienteDto>> ObtenerClientesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 2");
        }

        public Task<ClienteDto?> ObtenerClientePorIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 2");
        }

        public Task<Respuesta<ClienteDto>> CrearClienteAsync(ClienteDto cliente, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 2");
        }

        public Task<Respuesta<ClienteDto>> ActualizarClienteAsync(ClienteDto cliente, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 2");
        }

        public Task<Respuesta<ClienteDto>> EliminarClienteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 2");
        }
    }
}
