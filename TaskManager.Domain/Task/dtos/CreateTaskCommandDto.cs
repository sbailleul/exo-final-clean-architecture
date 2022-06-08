namespace TaskManager.Domain.Task.dtos;

public record CreateTaskCommandDto(
    string Description,
    string? Tag,
    DateTime? DueDate,
    DateTime? CloseDate,
    int? ParentId,
    string? State
    );