namespace TaskManager.Domain.Task;

public enum TaskState
{
    Todo, 
    Pending, 
    Progress, 
    Done, 
    Cancelled, 
    Closed
    
}


public record State(TaskState Value);