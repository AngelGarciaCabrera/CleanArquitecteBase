
using Microsoft.EntityFrameworkCore;


namespace  Application.Data;

public interface IApplicationDbContext
{

    DbSet<Domain.Usuario.Usuario> usuario { get; set; }
     DbSet<Domain.Tickets.Tickets> tickets { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
}