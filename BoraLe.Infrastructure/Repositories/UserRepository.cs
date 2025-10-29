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

        public async Task<UserLogin> GetByFieldAsync(string field, string value, string userId)
        {
            var query = _db.Collection(CollectionName).WhereEqualTo(field, value);

            if (!string.IsNullOrEmpty(userId))
                query = query.WhereNotEqualTo(FieldPath.DocumentId, userId);

            var snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count == 0)
                return null;

            return snapshot.Documents.First().ConvertTo<UserLogin>();
        }

        public async Task<string> UpsertUser(UpsertUser user)
        {
            var userRef = string.IsNullOrEmpty(user.Id)
                ? _db.Collection(CollectionName).Document()
                : _db.Collection(CollectionName).Document(user.Id);

            await userRef.SetAsync(new UserLogin
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                State = user.State,
                City = user.City,
                Pass = user.Pass
            });

            return userRef.Id.ToString();
        }

        public async Task<UserProfile> GetUser(string userId)
        {
            var docRef = _db.Collection(CollectionName).Document(userId);

            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return null;

            var user = snapshot.ConvertTo<User>();

            return new UserProfile
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                State = user.State,
                City = user.City
            };
        }
    }
}
