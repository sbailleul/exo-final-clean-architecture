namespace TaskManager.Infrastructure.Storage.contracts;

public class TaskDto
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? CloseCreated { get; set; }
    public String Description { get; set; } = "";
    public String State { get; set; }
    public String Tag { get; set; }
    public IEnumerable<TaskDto> SubTasks { get; set; } = new List<TaskDto>();
}