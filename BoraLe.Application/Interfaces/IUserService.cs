using BoraLe.Domain.Entities;

namespace BoraLe.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Register(Register register);
        Task<string> Login(string email, string pass);
        Task<UserProfile> GetUser(string userId);
    }
}
