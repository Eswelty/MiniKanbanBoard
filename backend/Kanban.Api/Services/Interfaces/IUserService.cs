using Kanban.Api.DTOs;
using Kanban.Api.Models;

namespace Kanban.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> CreateAsync(CreateUserDto dto);
        Task<User?> GetByIdAsync(int id);
        Task<User?> UpdateAsync(int id, CreateUserDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
