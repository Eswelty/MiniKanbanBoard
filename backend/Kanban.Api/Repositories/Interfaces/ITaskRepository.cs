using Kanban.Api.Models;

namespace Kanban.Api.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<List<KanbanTask>> GetAllAsync(int? assigneeId);
    Task<KanbanTask?> GetByIdAsync(int id);
    Task<KanbanTask> CreateAsync(KanbanTask task);
    Task SaveChangesAsync();
    Task DeleteAsync(KanbanTask task);
}