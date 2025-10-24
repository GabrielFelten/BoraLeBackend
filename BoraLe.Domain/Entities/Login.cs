using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace BoraLe.Domain.Entities
{
    public class Register
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(300, ErrorMessage = "O nome não pode ter mais de 300 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [MaxLength(500, ErrorMessage = "O e-mail não pode ter mais de 500 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [MaxLength(500, ErrorMessage = "A cidade não pode ter mais de 500 caracteres.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [MaxLength(300, ErrorMessage = "O telefone não pode ter mais de 300 caracteres.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MaxLength(25, ErrorMessage = "A senha não pode ter mais de 25 caracteres.")]
        public string Pass { get; set; }
    }

    [FirestoreData]
    public class UserLogin : User
    {        
        [FirestoreProperty]
        public string Pass { get; set; }
    }
}