namespace Kanban.Api.DTOs;

public record TaskDto(
    int Id,
    string Title,
    string? Description,
    string Status,
    int? AssigneeId
);