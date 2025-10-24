using BoraLe.Application.Interfaces;
using BoraLe.Domain.Entities;
using BoraLe.Domain.Interfaces;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace BoraLe.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<string> Register(Register register)
        {
            await ValidRegister(register);

            register.Pass = BCrypt.Net.BCrypt.HashPassword(register.Pass);

            return await _repo.AddAsync(register);
        }

        private async Task ValidRegister(Register register)
        {
            var userByEmail = await _repo.GetByFieldAsync("Email", register.Email);
            if (userByEmail is not null)
                throw new ArgumentException("Já existe um usuário com este e-mail.");

            var userByPhone = await _repo.GetByFieldAsync("Phone", register.Phone);
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
    }
}
