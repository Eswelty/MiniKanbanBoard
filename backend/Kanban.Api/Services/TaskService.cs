using Kanban.Api.DTOs;
using Kanban.Api.Models;
using Kanban.Api.Repositories.Interfaces;
using Kanban.Api.Services.Interfaces;

namespace Kanban.Api.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IUserRepository _userRepository;

    public TaskService(
        ITaskRepository repository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<List<TaskDto>> GetAllAsync(int? assigneeId)
    {
        var tasks = await _repository.GetAllAsync(assigneeId);
        return tasks.Select(ToDto).ToList();
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        return task == null ? null : ToDto(task);
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
    {
        await ValidateAssigneeAsync(dto.AssigneeId);

        var task = new KanbanTask
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            AssigneeId = dto.AssigneeId
        };

        return ToDto(await _repository.CreateAsync(task));
    }

    public async Task<TaskDto?> UpdateAsync(int id, CreateTaskDto dto)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return null;

        await ValidateAssigneeAsync(dto.AssigneeId);

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.AssigneeId = dto.AssigneeId;

        await _repository.SaveChangesAsync();

        return ToDto(task);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return false;

        await _repository.DeleteAsync(task);

        return true;
    }

    private async Task ValidateAssigneeAsync(int? id)
    {
        if (id.HasValue &&
            await _userRepository.GetByIdAsync(id.Value) == null)
        {
            throw new ArgumentException("Assignee not found.");
        }
    }

    private static TaskDto ToDto(KanbanTask task)
    {
        return new TaskDto(
            task.Id,
            task.Title,
            task.Description,
            task.Status,
            task.AssigneeId
        );
    }
}