using BoraLe.Domain.Entities;

namespace BoraLe.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<UserLogin> GetByFieldAsync(string field, string value);
        Task<string> AddAsync(Register register);        
    }
}
