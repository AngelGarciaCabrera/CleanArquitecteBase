namespace Domain.Tickets;



public interface ITicketsRepository
{
    Task<IEnumerable<Tickets>> GetAllAsync();
    Task<Tickets?> GetByIdAsnyc(int id);
    Task AddAsync(Tickets tickets);
    Task UpdateAsync(Tickets tickets);
    Task DeleteAsync(Tickets tickets);
    Task<List<Tickets>> GetByPageAsync(int page, int limit); 
    Task<List<Tickets>> GetByIdAMultiIdsAsync(List<int>ids);
    Task DeleteByIdsAsync(List<int> ids);
    Task<List<Tickets>> GetByUserIdAsync(int User_Id);
    Task<List<Tickets>>GetByDeparmentIdAsync(int Departamento_Id);
    Task<List<Tickets>> GetByDepartamentoAndPageAsync(int departamentoId, int page, int limit);
     Task<List<Tickets>> GetPagesByUserId(int user_Id, int page, int limit);
      Task<List<Tickets>> GetPagesByUserCreator(string userCreator, int page, int limit);


}