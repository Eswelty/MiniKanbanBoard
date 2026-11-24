using System;
using System.Collections.Generic;

namespace Kanban.Api.Models;

public partial class KanbanTask
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public int? AssigneeId { get; set; }

    public virtual User? Assignee { get; set; }
}
