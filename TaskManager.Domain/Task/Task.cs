namespace TaskManager.Domain.Task;

public class Task
{

    public TaskId Id { get; }
    public State State { get; }
    public string Description { get; }
    public string? Tag { get; }
    public DateTime CreationDate { get; }
    public DateTime? DueDate { get; }
    public DateTime? CloseDate { get; }
    public Task? Parent { get; }
    public IEnumerable<Task>? Children { get; }

    private Task(TaskId id, State state, string description, string? tag, DateTime creationDate, DateTime? dueDate, DateTime? closeDate, IEnumerable<Task>? children, Task? parent)
    {
        Id = id;
        State = state;
        Description = description;
        Tag = tag;
        CreationDate = creationDate;
        DueDate = dueDate;
        CloseDate = closeDate;
        Children = children;
        Parent = parent;
    }

    public static Task CreateNew(TaskId id, State? state, string description, string? tag, DateTime creationDate, DateTime? dueDate,
        DateTime? closeDate, IEnumerable<Task>? children, Task? parent)
    {
        state ??= new State(TaskState.Todo);

        return new Task(id, state, description, tag, creationDate, dueDate, closeDate, children, parent);
    }

    public Task? GetChildren(string compositeId)
    {
        if (Children is null) return null;

        var multipleIds = compositeId.Split(":").Select(int.Parse).ToList();
        var firstChildId = multipleIds[0];
        multipleIds.RemoveAt(0);
        var otherChildren = string.Join(':', multipleIds);

        var firstChild = Children.FirstOrDefault(c => c.Id.Value == firstChildId);

        if (firstChild is null) return null;

        if (otherChildren == "") return firstChild;
        
        return firstChild.GetChildren(otherChildren);
    }
}   



