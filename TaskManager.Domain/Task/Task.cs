using TaskManager.Domain.Task.dtos;

namespace TaskManager.Domain.Task;

public class Task
{

    public TaskId Id { get; }
    public State State {  get; private set; }
    public string Description { get; private set; }
    public string? Tag { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CloseDate { get; private set;}
    public Task? Parent { get; private set;}
    public IEnumerable<Task>? Children { get; private set;}

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
        state ??= State.Todo();

        return new Task(id, state, description, tag, creationDate, dueDate, closeDate, children, parent);
    }

    private int GetFirstId(string compositeId)
    {
        return compositeId.Split(":").Select(int.Parse).ToList()[0];
    }

    private string GetChildrenIds(string compositeId)
    {
        var multipleIds = compositeId.Split(":").Select(int.Parse).ToList();
        multipleIds.RemoveAt(0);
        return string.Join(':', multipleIds);
    }

    public Task? GetChildren(string compositeId)
    {
        if (Children is null) return null;

        var firstChildId = GetFirstId(compositeId);
        var otherChildrenIds = GetChildrenIds(compositeId);

        var firstChild = Children.FirstOrDefault(c => c.Id.Value == firstChildId);

        if (firstChild is null) return null;

        if (otherChildrenIds == "") return firstChild;
        
        return firstChild.GetChildren(otherChildrenIds);
    }

    public void AddChild(CreateTaskCommandDto dto)
    {
        throw new NotImplementedException();
    }
    
    public void Update(UpdateTaskCommandDto dto, DateTime currentDate)
    {
        if (dto.Description is not null && dto.Description != Description) UpdateDescription(dto.Description);
        if (dto.State is not null && dto.State != State.Value) UpdateState(dto.State, currentDate);
        if (dto.Tag is not null && dto.Tag != Tag) UpdateTag(dto.Tag);
        if (dto.DueDate is not null && dto.DueDate != DueDate) UpdateDueDate(dto.DueDate.Value);
    }

    private void UpdateDueDate(DateTime dtoDueDate)
    {
        DueDate = dtoDueDate;
    }

    private void UpdateTag(string dtoTag)
    {
        Tag = dtoTag;
    }

    private void UpdateState(string newState, DateTime currentDate)
    {
        if (new State(newState) == State.Closed()) OnCloseTask(currentDate);
        State = new State(newState);
    }

    private void OnCloseTask(DateTime currentDate)
    {
        CloseDate = currentDate;
    }

    private void UpdateDescription(string dtoDescription)
    {
        Description = dtoDescription;
    }
}   



