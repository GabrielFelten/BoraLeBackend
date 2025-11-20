using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace BoraLe.Domain.Entities
{
    public class UpsertUser
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O nome não pode ter mais de 255 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [MaxLength(255, ErrorMessage = "O e-mail não pode ter mais de 255 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [MaxLength(2, ErrorMessage = "O estado não pode ter mais de 2 caracteres.")]
        public string State { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [MaxLength(300, ErrorMessage = "A cidade não pode ter mais de 300 caracteres.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O CEP não pode ter mais de 50 caracteres.")]
        public string Cep { get; set; }

        [MaxLength(300, ErrorMessage = "A rua não pode ter mais de 300 caracteres.")]
        public string Street { get; set; }

        [MaxLength(50, ErrorMessage = "O número não pode ter mais de 50 caracteres.")]
        public string Number { get; set; }

        [MaxLength(200, ErrorMessage = "O bairro não pode ter mais de 200 caracteres.")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O telefone não pode ter mais de 255 caracteres.")]
        public string Phone { get; set; }        

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public string Type { get; set; }
        public bool PublicContact { get; set; }

        [MaxLength(25, ErrorMessage = "A senha não pode ter mais de 25 caracteres.")]
        public string Pass { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(Pass))
            {
                yield return new ValidationResult(
                    "A senha é obrigatória para novos usuários.",
                    new[] { nameof(Pass) }
                );
            }
        }
    }

    [FirestoreData]
    public class UserLogin : User
    {        
        [FirestoreProperty]
        public string Pass { get; set; }
    }
}