namespace TaskManager.Domain.Task.dtos;

public record AddSubTaskCommandDto(
    string Description,
    string? Tag,
    DateTime? DueDate,
    string CompleteParentId,
    string? State
);