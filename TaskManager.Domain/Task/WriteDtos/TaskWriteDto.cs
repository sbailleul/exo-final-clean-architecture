namespace TaskManager.Domain.Task.WriteDtos;

public record TaskWriteDto(
    int Id,
    string Description,
    string? Tag,
    string State,
    DateTime CreationDate,
    DateTime? DueDate,
    DateTime? CloseDate,
    IEnumerable<Task> Children
    );