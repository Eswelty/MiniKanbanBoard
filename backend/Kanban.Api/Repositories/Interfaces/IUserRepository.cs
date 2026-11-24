using Kanban.Api.Models;

namespace Kanban.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task SaveChangesAsync();
        Task DeleteAsync(User user);
    }
}
