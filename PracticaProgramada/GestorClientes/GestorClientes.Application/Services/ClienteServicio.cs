using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GestorClientes.Application.Dtos;
using GestorClientes.Infrastructure.Repository;

namespace GestorClientes.Application.Services
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly AutoMapper.IMapper _mapper;

        public ClienteServicio(IClienteRepository clienteRepository, AutoMapper.IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> ObtenerClientesAsync(CancellationToken cancellationToken = default)
        {
            var clientes = await _clienteRepository.GetAllClientesAsync(cancellationToken);
            return _mapper.Map<List<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto?> ObtenerClientePorIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id, cancellationToken);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<Respuesta<ClienteDto>> CrearClienteAsync(ClienteDto cliente, CancellationToken cancellationToken = default)
        {
            var respuesta = new Respuesta<ClienteDto>();
            var clienteEntity = _mapper.Map<Domain.Cliente>(cliente);

            if (!clienteEntity.ValidarReglasDeNegocio())
            {
                respuesta.EsCorrecto = false;
                respuesta.Mensaje = "La cédula, el nombre y al menos un teléfono son obligatorios.";
                respuesta.Codigo = 400;
                return respuesta;
            }

            clienteEntity.Cedula = clienteEntity.Cedula.Trim();

            if (await _clienteRepository.ExisteCedulaAsync(clienteEntity.Cedula, cancellationToken: cancellationToken))
            {
                respuesta.EsCorrecto = false;
                respuesta.Mensaje = "Ya existe un cliente registrado con esa cédula.";
                respuesta.Codigo = 409;
                return respuesta;
            }

            if (!await _clienteRepository.CreateClienteAsync(clienteEntity, cancellationToken))
            {
                respuesta.EsCorrecto = false;
                respuesta.Mensaje = "No se pudo crear el cliente.";
                respuesta.Codigo = 500;
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<ClienteDto>(clienteEntity);

            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> ActualizarClienteAsync(ClienteDto cliente, CancellationToken cancellationToken = default)
        {
            var respuesta = new Respuesta<ClienteDto>();
            var clienteEntity = _mapper.Map<Domain.Cliente>(cliente);

            if (!clienteEntity.ValidarReglasDeNegocio())
            {
                respuesta.EsCorrecto = false;
                respuesta.Mensaje = "La cédula, el nombre y al menos un teléfono son obligatorios.";
                respuesta.Codigo = 400;
                return respuesta;
            }

            clienteEntity.Cedula = clienteEntity.Cedula.Trim();

            if (await _clienteRepository.ExisteCedulaAsync(clienteEntity.Cedula, clienteEntity.Id, cancellationToken))
            {
                respuesta.EsCorrecto = false;
                respuesta.Mensaje = "Esa cédula ya está registrada por otro cliente.";
                respuesta.Codigo = 409;
                return respuesta;
            }

            if (!await _clienteRepository.UpdateClienteAsync(clienteEntity, cancellationToken))
            {
                respuesta.EsCorrecto = false;
                respuesta.Mensaje = "No se pudo actualizar el cliente.";
                respuesta.Codigo = 500;
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<ClienteDto>(clienteEntity);
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> EliminarClienteAsync(int id, CancellationToken cancellationToken = default)
        {
            var respuesta = new Respuesta<ClienteDto>();

            if (!await _clienteRepository.DeleteClienteAsync(id, cancellationToken))
            {
                respuesta.EsCorrecto = false;
                respuesta.Mensaje = "No se pudo eliminar el cliente.";
                respuesta.Codigo = 500;
            }

            return respuesta;
        }
    }
}