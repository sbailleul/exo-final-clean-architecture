namespace TaskManager.Domain.Task.WriteDtos;

public record TaskReadDto(
    int Id,
    string Description,
    string? Tag,
    string State,
    DateTime CreationDate,
    DateTime? DueDate,
    DateTime? CloseDate,
    IEnumerable<Task> Children);