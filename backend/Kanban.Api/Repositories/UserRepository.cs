using Kanban.Api.Data;
using Kanban.Api.Models;
using Kanban.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KanbanDbContext _context;

        public UserRepository(KanbanDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
