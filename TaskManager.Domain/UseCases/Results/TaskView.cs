namespace TaskManager.Domain.UseCases.Results;

public record TaskView(
    int Id,
    string Description,
    string? Tag,
    DateTime? CreationDate,
    DateTime? DueDate,
    DateTime? CloseDate,
    List<int> Children);