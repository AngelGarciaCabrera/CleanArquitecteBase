using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Usuario
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id); 
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
        Task<Usuario?> GetByCorreoAsync(string correo);
        Task<List<Usuario?>> GetByUsersByDeparmentIdAsync(int Departamento_Id);
    }
}
