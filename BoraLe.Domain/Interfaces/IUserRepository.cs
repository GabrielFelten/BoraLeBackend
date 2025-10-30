using BoraLe.Domain.Entities;

namespace BoraLe.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<UserLogin> GetByFieldAsync(string field, string value, string userId = null);
        Task<string> UpsertUser(UpsertUser user);
        Task<UserProfile> GetUser(string userId);
        Task<string> GetUserPass(string userId);
    }
}
