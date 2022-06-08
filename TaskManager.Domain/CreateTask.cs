namespace TaskManager.Domain;

public class CreateTask: IMiddleware<CreateTask, Unit>
{
    public Unit Next(CreateTask request)
    {
        return Unit.Value;
    }
}