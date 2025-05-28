
namespace Infraestructure.Persistence.Repositories
{
    using Application.Data;
    using Domain.Usuario;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IApplicationDbContext _context;

        public UsuarioRepository(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Usuario usuario) => await _context.usuario.AddAsync(usuario);

        public async Task<Usuario?> GetByIdAsync(int id) => await _context.usuario.SingleOrDefaultAsync(X => X.Id == id);

        public async Task<IEnumerable<Usuario>> GetAllAsync() => await _context.usuario.ToListAsync();

        public async Task<Usuario?> GetByCorreoAsync(string correo)
        {
            return (await _context.usuario.ToListAsync())
                .FirstOrDefault(x => x.Correo!.ToString() == correo);
        }


        public async Task UpdateAsync(Usuario usuario)
        {
            _context.usuario.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            _context.usuario.Remove(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Usuario?>> GetByUsersByDeparmentIdAsync(int Departamento_Id)
        {
            return await _context.usuario
                .Where(u=> u.Departamento_id == Departamento_Id)
                .ToListAsync();
        }
        
    }
}