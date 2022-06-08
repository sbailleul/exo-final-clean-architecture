using TaskManager.Infrastructure.Storage.contracts.Persistence;

namespace TaskManager.Infrastructure.Storage.contracts.Tasks;

public class TaskEntity: IEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public String Description { get; set; } = "";
    public String State { get; set; }
    public String? Tag { get; set; }
    public IEnumerable<TaskEntity> SubTasks { get; set; } = new List<TaskEntity>();
    
    public int GetId()
    {
        return Id;
    }
}