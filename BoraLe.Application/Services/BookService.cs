using BoraLe.Application.Interfaces;
using BoraLe.Domain.Entities;
using BoraLe.Domain.Interfaces;
using Google.Cloud.Firestore;

namespace BoraLe.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        public BookService(IBookRepository repo) => _repo = repo;

        public async Task UpsertBook(UpsertBook book)
        {
            await _repo.UpsertBook(book);
        }

        public async Task<IEnumerable<BooksUser>> GetBooksByUser(string userId)
        {
            var books = await _repo.GetByUserAsync(userId);

            var booksUser = new List<BooksUser>();

            foreach (var book in books)
            {
                booksUser.Add(new BooksUser
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    Objectives = book.Objectives,
                    Status = book.Status,
                    UserId = userId
                });
            }

            return booksUser;
        }

        public async Task DeleteBookAsync(string id)
        {
            await _repo.DeleteBookAsync(id);
        }
    }
}
