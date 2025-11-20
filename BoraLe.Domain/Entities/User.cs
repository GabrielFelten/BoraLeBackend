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

        [FirestoreProperty]
        public string Cep { get; set; }

        [FirestoreProperty]
        public string Street { get; set; }

        [FirestoreProperty]
        public string Number { get; set; }

        [FirestoreProperty]
        public string Neighborhood { get; set; }

        [FirestoreProperty]
        public bool PublicContact { get; set; }

        [FirestoreProperty]
        public string Type { get; set; }

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
        public string Cep { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public bool PublicContact { get; set; }
        public string Type { get; set; }
    }

    public enum enumTypePerson
    {
        PF,
        PJ
    }
}