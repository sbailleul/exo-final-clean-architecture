using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Task.dtos;
using TaskManager.Domain.Task.WriteDtos;

namespace TaskManager.Domain.Task;

public class Task
{
    public TaskId Id { get; }
    public State State { get; private set; }
    public string Description { get; private set; }
    public string? Tag { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CloseDate { get; private set; }
    public IList<Task> Children { get; private set; }

    private Task(TaskId id, State state, string description, string? tag, DateTime creationDate, DateTime? dueDate,
        DateTime? closeDate, IEnumerable<Task> children)
    {
        Id = id;
        State = state;
        Description = description;
        Tag = tag;
        CreationDate = creationDate;
        DueDate = dueDate;
        CloseDate = closeDate;
        Children = children.ToList();
    }

    public static Task CreateNew(TaskId id, State? state, string description, string? tag, DateTime creationDate,
        DateTime? dueDate,
        DateTime? closeDate)
    {
        state ??= State.Todo();

        return new Task(id, state, description, tag, creationDate, dueDate, closeDate, new List<Task>());
    }

    public Task? GetChild(CompleteParentId compositeId)
    {
        if (Children.Any() == false) return null;

        var firstChildId = compositeId.GetFirstId();
        var otherChildrenIds = compositeId.GetChildrenIds();

        var firstChild = Children.FirstOrDefault(c => c.Id.Value == firstChildId);

        if (firstChild is null) return null;

        if (otherChildrenIds.IsEmpty()) return firstChild;

        return firstChild.GetChild(otherChildrenIds);
    }

    public Task? GetParentOf(CompleteParentId compositeId)
    {
        if (Children.Any() == false) return null;

        if (compositeId.GetChildrenIds().IsEmpty()) return this;

        var firstChildId = compositeId.GetFirstId();
        var otherChildrenIds = compositeId.GetChildrenIds();

        var firstChild = Children.FirstOrDefault(c => c.Id.Value == firstChildId);

        if (firstChild is null) return null;

        if (otherChildrenIds.IsEmpty()) return firstChild;

        return firstChild.GetChild(otherChildrenIds);
    }

    // when outside of Class, AddChild should always be called on Root Task
    public void AddChild(Task newTask, CompleteParentId completeParentId)
    {
        if (completeParentId is null) throw new ArgumentException();

        Task? nodeToAttach;
        if (completeParentId.Value == Id.Value.ToString())
        {
            nodeToAttach = this;
        }
        else
        {
            nodeToAttach = GetChild(completeParentId.GetChildrenIds());
        }

        if (nodeToAttach is null) throw new TaskNotFindableInTaskException();

        if (nodeToAttach.Children.Any(c => c.Id == newTask.Id)) throw new ChildrenTaskConflictException();

        nodeToAttach.Children.Add(newTask);
    }

    public void DeleteChildren(CompleteParentId parentFullId)
    {
        if (parentFullId is null) throw new ArgumentException();

        var taskToDeleteId = parentFullId.GetLast();
        Task? parentTask = GetParentOf(parentFullId.GetChildrenIds().GetLastFullParentId());

        if (parentTask is null) throw new TaskNotFindableInTaskException();

        var taskToDelete = parentTask.Children.FirstOrDefault(task => task.Id.Value == taskToDeleteId);

        if (taskToDelete == null) throw new ChildrenNotFoundException();

        parentTask.Children.Remove(taskToDelete);
    }

    public void UpdateChildren(UpdateTaskCommandDto request, CompleteParentId fullParentId)
    {
        if (fullParentId is null) throw new ArgumentException();
        var taskToUpdate = fullParentId.GetLast();
        var childToUpdate = GetChild(fullParentId.GetChildrenIds());
        if (childToUpdate is null) throw new TaskNotFindableInTaskException();
        
        childToUpdate.Update(request, DateTime.Now);
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

    public static Task From(TaskReadDto dto)
    {
        return new Task(
            new TaskId(dto.Id),
            new State(dto.State),
            dto.Description,
            dto.Tag,
            dto.CreationDate,
            dto.DueDate,
            dto.CloseDate,
            dto.Children.Select(From)
        );
    }

    public TaskWriteDto ToWriteDto()
    {
        return new TaskWriteDto(
            Id.Value,
            Description,
            Tag,
            State.Value,
            CreationDate,
            DueDate,
            CloseDate,
            Children.Select(c => c.ToWriteDto())
        );
    }
}