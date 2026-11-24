using System.ComponentModel.DataAnnotations;

namespace Kanban.Api.DTOs;

public class CreateTaskDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [RegularExpression("^(To Do|In Progress|Done)$")]
    public string Status { get; set; } = "To Do";

    public int? AssigneeId { get; set; }
}