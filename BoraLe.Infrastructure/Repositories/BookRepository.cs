using Google.Cloud.Firestore;
using BoraLe.Domain.Entities;
using BoraLe.Domain.Interfaces;
using BoraLe.Infrastructure.Config;

namespace BoraLe.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly FirestoreDb _db;
        private const string CollectionName = "book";

        public BookRepository(FirebaseConfig firebaseConfig)
        {
            _db = firebaseConfig.GetFirestoreDb();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();
            return snapshot.Documents.Select(doc => doc.ConvertTo<Book>()).ToList();
        }

        public async Task<string> UpsertBook(UpsertBook book)
        {
            var bookRef = string.IsNullOrEmpty(book.Id)
                ? _db.Collection(CollectionName).Document()
                : _db.Collection(CollectionName).Document(book.Id);

            var userRef = _db.Collection("users").Document(book.IdUser);

            await bookRef.SetAsync(new Book
            {
                Title = book.Title,
                Genre = book.Genre,
                Status = book.Status,
                Objectives = book.Objectives,
                UserRef = userRef
            });

            return bookRef.Id.ToString();
        }

        public async Task<IEnumerable<Book>> GetByUserAsync(string userId)
        {
            var userRef = _db.Collection("users").Document(userId);

            var snapshot = await _db.Collection(CollectionName)
                .WhereEqualTo("UserId", userRef)
                .GetSnapshotAsync();

            var books = snapshot.Documents
                .Select(doc => doc.ConvertTo<Book>())
                .ToList();

            return books;
        }

        public async Task DeleteBookAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            var bookRef = _db.Collection(CollectionName).Document(id);

            var snapshot = await bookRef.GetSnapshotAsync();
            if (!snapshot.Exists)
                return;

            await bookRef.DeleteAsync();
        }
    }
}
