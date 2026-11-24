using Kanban.Api.DTOs;
using Kanban.Api.Models;
using Kanban.Api.Repositories.Interfaces;
using Kanban.Api.Services.Interfaces;

namespace Kanban.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = await _repository.GetAllAsync();
            return users;
        }

        public async Task<User> CreateAsync(CreateUserDto dto)
        {
            User user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
            };

            User createdUser = await _repository.CreateAsync(user);

            return createdUser;
        }

        public async Task<User?> UpdateAsync(int id, CreateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return null;

            user.Name = dto.Name;
            user.Email = dto.Email;

            await _repository.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _repository.DeleteAsync(user);

            return true;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
