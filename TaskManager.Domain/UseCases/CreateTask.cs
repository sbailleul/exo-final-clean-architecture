using TaskManager.Domain.Task.dtos;

namespace TaskManager.Domain.UseCases;

public class CreateTask: IMiddleware<CreateTaskCommandDto, Unit>
{
    public Unit Next(CreateTaskCommandDto request)
    {
        return Unit.Value;
    }
}