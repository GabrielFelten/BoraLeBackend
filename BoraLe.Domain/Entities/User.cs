using Google.Cloud.Firestore;

namespace BoraLe.Domain.Entities
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string Phone { get; set; }

        [FirestoreProperty]
        public string State { get; set; }

        [FirestoreProperty]
        public string City { get; set; }

        [FirestoreDocumentId]
        public string Id { get; set; }
    }

    public class UserProfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}