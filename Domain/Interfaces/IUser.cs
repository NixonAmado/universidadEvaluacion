using Domain.Entities;

namespace Domain.Interfaces;

public interface IUser : IGenericRepository<User>
{
    Task<User> GetByUserNameAsync(string username);
    Task<User> GetByRefreshTokenAsync(string username);
    Task<IEnumerable<User>> GetAllRolesAsync();
}