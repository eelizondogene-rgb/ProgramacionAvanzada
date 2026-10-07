using GestorClientes.Domain;

namespace GestorClientes.Infrastructure.Repository
{
    public interface IClienteRepository
    {
        /// <summary>Devuelve todos los clientes con sus teléfonos.</summary>
        Task<List<Cliente>> GetAllClientesAsync(CancellationToken cancellationToken = default);

        /// <summary>Devuelve un cliente con sus teléfonos, o null si no existe.</summary>
        Task<Cliente?> GetClienteByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>Guarda un cliente nuevo junto con sus teléfonos. True si se guardó.</summary>
        Task<bool> CreateClienteAsync(Cliente cliente, CancellationToken cancellationToken = default);

        /// <summary>Modifica un cliente existente y su lista de teléfonos. True si se guardó.</summary>
        Task<bool> UpdateClienteAsync(Cliente cliente, CancellationToken cancellationToken = default);

        /// <summary>Borra un cliente y sus teléfonos. True si se borró.</summary>
        Task<bool> DeleteClienteAsync(int id, CancellationToken cancellationToken = default);
    }
}
