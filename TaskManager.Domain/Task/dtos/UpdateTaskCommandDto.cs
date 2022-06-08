namespace TaskManager.Domain.Task.dtos;

public record UpdateTaskCommandDto(
    string CompleteId,
    string? Description,
    string? Tag,
    DateTime? DueDate,
    string? State
    );