using BoraLe.Domain.Entities;

namespace BoraLe.Application.Interfaces
{
    public interface IBookService
    {
        Task UpsertBook(UpsertBook book);
        Task<IEnumerable<BooksUser>> GetBooksByUser(string userId);

        Task DeleteBookAsync(string id);
    }
}
