using BoraLe.Application.Interfaces;
using BoraLe.Domain.Entities;
using BoraLe.Domain.Interfaces;

namespace BoraLe.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<string> UpsertUser(UpsertUser user)
        {
            await ValidUpsertUser(user);

            if (string.IsNullOrEmpty(user.Id))
                user.Pass = BCrypt.Net.BCrypt.HashPassword(user.Pass);
            else
                user.Pass = await _repo.GetUserPass(user.Id);

            return await _repo.UpsertUser(user);
        }

        private async Task ValidUpsertUser(UpsertUser user)
        {
            var userByEmail = await _repo.GetByFieldAsync("Email", user.Email, user.Id);
            if (userByEmail is not null)
                throw new ArgumentException("Já existe um usuário com este e-mail.");

            var userByPhone = await _repo.GetByFieldAsync("Phone", user.Phone, user.Id);
            if (userByPhone is not null)
                throw new ArgumentException("Já existe um usuário com este telefone.");
        }

        public async Task<string> Login(string email, string pass)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Preencha o e-mail.");

            if (string.IsNullOrEmpty(pass))
                throw new ArgumentException("Preencha a senha.");

            var userLogin = await _repo.GetByFieldAsync("Email", email);
            if (userLogin is null)
                throw new ArgumentException("Não existe um usuário com este e-mail.");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(pass, userLogin.Pass);
            if (!senhaValida)
                throw new ArgumentException("Senha inválida.");

            Console.WriteLine($"Usuário {userLogin.Name} logado com sucesso!");

            return userLogin.Id;
        }

        public async Task<UserProfile> GetUser(string userId)
        {
            return await _repo.GetUser(userId);
        }
    }
}
