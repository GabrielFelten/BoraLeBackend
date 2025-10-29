using BoraLe.Domain.Entities;

namespace BoraLe.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> UpsertUser(UpsertUser user);
        Task<string> Login(string email, string pass);
        Task<UserProfile> GetUser(string userId);
    }
}
