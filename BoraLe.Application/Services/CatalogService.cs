using BoraLe.Application.Interfaces;
using BoraLe.Domain.Entities;
using BoraLe.Domain.Interfaces;

namespace BoraLe.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IUserRepository _repoUser;
        private readonly IBookRepository _repoBook;
        public CatalogService(IUserRepository repoUser, IBookRepository repoBook)
        {
            _repoUser = repoUser;
            _repoBook = repoBook;
        }

        public async Task<IEnumerable<Catalog>> ListCatalogAsync(List<string> Objectives, string genre = null, string title = null, string city = null)
        {
            var books = await _repoBook.GetAllAsync();

            var users = (await _repoUser.GetAllAsync())
                .Where(u => string.IsNullOrEmpty(city) || u.City.Contains(city, StringComparison.OrdinalIgnoreCase));

            var usersDictionary = users.ToDictionary(u => u.Id);

            var query = books
                .Where(b => b.Status)
                .Where(b => string.IsNullOrEmpty(genre) || b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase))
                .Where(b => string.IsNullOrEmpty(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Where(b => Objectives == null || Objectives.Count == 0 || b.Objectives.Any(o => Objectives.Contains(o)))
                .ToList();

            var catalog = new List<Catalog>();

            foreach (var book in query)
            {
                if (usersDictionary.TryGetValue(book.UserId, out var user))
                {
                    catalog.Add(new Catalog
                    {
                        Title = book.Title,
                        Genre = book.Genre,
                        Objectives = book.Objectives,
                        Status = book.Status,
                        UserName = user.Name,
                        UserState = user.State,
                        UserCity = user.City,
                        UserPhone = user.Phone,
                        UserEmail = user.Email
                    });
                }
            }

            return catalog;
        }
    }
}
