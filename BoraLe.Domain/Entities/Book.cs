using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace BoraLe.Domain.Entities
{
    [FirestoreData]
    public class Book
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Title { get; set; }

        [FirestoreProperty]
        public string Genre { get; set; }

        [FirestoreProperty]
        public bool Status { get; set; }

        [FirestoreProperty]
        public List<string> Objectives { get; set; }

        [FirestoreProperty("UserId")]
        public DocumentReference UserRef { get; set; }

        public string UserId => UserRef?.Id;
    }

    public class UpsertBook
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório.")]
        [MaxLength(300, ErrorMessage = "O Título não pode ter mais de 300 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O Gênero é obrigatório.")]
        [MaxLength(300, ErrorMessage = "O Gênero não pode ter mais de 300 caracteres.")]
        public string Genre { get; set; }

        public bool Status { get; set; }

        public List<enumObjectives> Objectives { get; set; }

        [Required(ErrorMessage = "O IdUser é obrigatório.")]
        public string IdUser { get; set; }
    }

    public class BooksUser
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public bool Status { get; set; }
        public List<enumObjectives> Objectives { get; set; }
        public string UserId { get; set; }
    }

    public enum enumObjectives
    {
        Exchange,
        Loan,
        Donation
    }
}