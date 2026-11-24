using Kanban.Api.Data;
using Kanban.Api.Models;
using Kanban.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Api.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly KanbanDbContext _context;

    public TaskRepository(KanbanDbContext context)
    {
        _context = context;
    }

    public async Task<List<KanbanTask>> GetAllAsync(int? assigneeId)
    {
        var query = _context.KanbanTasks.AsQueryable();

        if (assigneeId.HasValue)
            query = query.Where(t => t.AssigneeId == assigneeId.Value);

        return await query.ToListAsync();
    }

    public async Task<KanbanTask?> GetByIdAsync(int id)
    {
        return await _context.KanbanTasks.FindAsync(id);
    }

    public async Task<KanbanTask> CreateAsync(KanbanTask task)
    {
        _context.KanbanTasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(KanbanTask task)
    {
        _context.KanbanTasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}