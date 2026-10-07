using GestorClientes.Domain;
using GestorClientes.Infrastructure.Data;

namespace GestorClientes.Infrastructure.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context) // Inyección de dependencias
        {
            _context = context;
        }

        // TODO (Persona 1): implementar los 5 métodos. Usar async, AsNoTracking en las consultas,
        // Include(c => c.Telefonos) para traer los teléfonos y pasar el CancellationToken.

        public Task<List<Cliente>> GetAllClientesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 1");
        }

        public Task<Cliente?> GetClienteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 1");
        }

        public Task<bool> CreateClienteAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 1");
        }

        public Task<bool> UpdateClienteAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 1");
        }

        public Task<bool> DeleteClienteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Pendiente: Persona 1");
        }
    }
}
