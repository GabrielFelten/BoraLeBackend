using Google.Cloud.Firestore;
using BoraLe.Domain.Entities;
using BoraLe.Domain.Interfaces;
using BoraLe.Infrastructure.Config;

namespace BoraLe.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FirestoreDb _db;
        private const string CollectionName = "users";

        public UserRepository(FirebaseConfig firebaseConfig)
        {
            _db = firebaseConfig.GetFirestoreDb();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();
            return snapshot.Documents.Select(doc => doc.ConvertTo<User>()).ToList();
        }

        public async Task<UserLogin> GetByFieldAsync(string field, string value)
        {
            var query = _db.Collection(CollectionName).WhereEqualTo(field, value);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Count == 0 ? null : snapshot.Documents.First().ConvertTo<UserLogin>();
        }

        public async Task<string> AddAsync(Register register)
        {
            var userRef = _db.Collection(CollectionName).Document();
            await userRef.SetAsync(new UserLogin
            {
                Name = register.Name,
                Email = register.Email,
                City = register.City,
                Phone = register.Phone,
                Pass = register.Pass
            });

            return userRef.Id.ToString();
        }
    }
}
