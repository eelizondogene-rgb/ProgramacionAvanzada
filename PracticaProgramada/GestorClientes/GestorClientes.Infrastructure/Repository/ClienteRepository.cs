
using Microsoft.EntityFrameworkCore;
using GestorClientes.Domain;
using GestorClientes.Infrastructure.Data;

namespace GestorClientes.Infrastructure.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllClientesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .Include(c => c.Telefonos)
                .ToListAsync(cancellationToken);
        }

        public async Task<Cliente?> GetClienteByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .Include(c => c.Telefonos)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<bool> CreateClienteAsync(
            Cliente cliente, CancellationToken cancellationToken = default)
        {
            _context.Clientes.Add(cliente);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateClienteAsync(
            Cliente cliente, CancellationToken cancellationToken = default)
        {
            var existente = await _context.Clientes
                .Include(c => c.Telefonos)
                .FirstOrDefaultAsync(c => c.Id == cliente.Id, cancellationToken);

            if (existente == null)
                return false;

            existente.Cedula = cliente.Cedula;
            existente.Nombre = cliente.Nombre;
            existente.Apellidos = cliente.Apellidos;

            _context.Telefonos.RemoveRange(existente.Telefonos);

            foreach (var telefono in cliente.Telefonos)
            {
                _context.Telefonos.Add(new Telefono
                {
                    Numero = telefono.Numero,
                    ClienteId = existente.Id
                });
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteClienteAsync(
            int id, CancellationToken cancellationToken = default)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
