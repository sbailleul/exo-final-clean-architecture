namespace TaskManager.Domain.Task.dtos;

public record CreateTaskCommandDto(
    string Description,
    string? Tag,
    DateTime? DueDate,
    string? CompleteParentId,
    string? State
    );