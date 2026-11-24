using Kanban.Api.DTOs;

namespace Kanban.Api.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskDto>> GetAllAsync(int? assigneeId);
    Task<TaskDto?> GetByIdAsync(int id);
    Task<TaskDto> CreateAsync(CreateTaskDto dto);
    Task<TaskDto?> UpdateAsync(int id, CreateTaskDto dto);
    Task<bool> DeleteAsync(int id);
}