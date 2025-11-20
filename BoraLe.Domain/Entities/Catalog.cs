using Google.Cloud.Firestore;

namespace BoraLe.Domain.Entities
{
    [FirestoreData]
    public class Catalog
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public bool Status { get; set; }
        public List<string> Objectives { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }        
        public string UserState { get; set; }        
        public string UserCity { get; set; }        
        public string UserPhone { get; set; }        
        public string UserEmail { get; set; }        
        public string UserCep { get; set; }        
        public string UserStreet { get; set; }        
        public string UserNumber { get; set; }        
        public string UserNeighborhood { get; set; }        
        public string UserType { get; set; }        
        public bool UserPublicContact { get; set; }        
    }
}