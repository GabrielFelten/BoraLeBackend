using BoraLe.Domain.Entities;

namespace BoraLe.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<string> UpsertBook(UpsertBook book);
        Task<IEnumerable<Book>> GetByUserAsync(string userId);
        Task DeleteBookAsync(string id);
    }
}
