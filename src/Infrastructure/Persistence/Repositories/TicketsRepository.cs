using Application.Data;
using Domain.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;


public class TicketsRepository : ITicketsRepository
{
    private readonly IApplicationDbContext _context;

    public TicketsRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task AddAsync(Tickets tickets) => await _context.tickets.AddAsync(tickets);

    public async Task<Tickets?> GetByIdAsnyc(int id) => await _context.tickets.SingleOrDefaultAsync(X => X.Id == id);

    public async Task<IEnumerable<Tickets>> GetAllAsync() => await _context.tickets.ToListAsync();

    public async Task UpdateAsync(Tickets tickets)
    {
        _context.tickets.Update(tickets);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Tickets tickets)
    {
        _context.tickets.Remove(tickets);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Tickets>> GetByPageAsync(int page, int limit)
    {
        int offset = (page - 1) * limit;

        return await _context.tickets
            .OrderByDescending(s => s.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }


    public async Task<List<Tickets>> GetByIdAMultiIdsAsync(List<int> ids)
    {
        if (ids == null || !ids.Any())
        {
            throw new ArgumentException("La lista de IDs no puede estar vacía.", nameof(ids));
        }

        return await _context.tickets
            .Where(s => ids.Contains(s.Id)) // 🔍 Filtra por los IDs proporcionados
            .ToListAsync();
    }

    public async Task DeleteByIdsAsync(List<int> ids)
    {
        if (ids == null || !ids.Any())
        {
            throw new ArgumentException("La lista de IDs no puede estar vacía.", nameof(ids));
        }

        foreach (var id in ids) //por cada id lo elimino uno a 1 ya que el entity framework me daba error
        {
            var Tickets = await _context.tickets.FindAsync(id);
            if (Tickets != null)
            {
                _context.tickets.Remove(Tickets);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Tickets>> GetByUserIdAsync(int User_Id)
    {
        return await _context.tickets
            .Where(u => u.User_Id == User_Id)
            .ToListAsync();
    }

    public async Task<List<Tickets>> GetByDeparmentIdAsync(int Departamento_Id)
    {
        return await _context.tickets
            .Where(t => t.Departamento_Id == Departamento_Id)
            .ToListAsync();
    }
    //paginado pero por departamentoId
    public async Task<List<Tickets>> GetByDepartamentoAndPageAsync(int departamentoId, int page, int limit)
    {
        int offset = (page - 1) * limit;

        return await _context.tickets
            .Where(t => t.Departamento_Id == departamentoId)
            .OrderByDescending(t => t.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }
    //paginado pero por user_Id,
    public async Task<List<Tickets>> GetPagesByUserId(int user_Id, int page, int limit)
    {
        int offset = (page - 1) * limit;

        return await _context.tickets
            .Where(t => t.User_Id == user_Id)
            .OrderByDescending(t => t.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }
    public async Task<List<Tickets>> GetPagesByUserCreator(string usuarioCorreo, int page, int limit)
    {
        int offset = (page - 1) * limit;

        return await _context.tickets
            .Where(t => t.CreadoPor != null && t.CreadoPor.Value == usuarioCorreo)

            .OrderByDescending(t => t.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }






}