using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Task;
using TaskManager.Domain.Task.dtos;

namespace TaskManager.Domain.UseCases;

public class UpdateTask
{
    private readonly ITasks _tasks;

    public UpdateTask(ITasks tasks)
    {
        _tasks = tasks;
    }
    
    public async Task<int> Execute(UpdateTaskCommandDto request)
    {
        var compositeId = new CompleteParentId(request.CompleteId);
        var parentDto = await _tasks.FindOne(compositeId.GetFirstId());

        if (parentDto == null) throw new ParentNotFoundException();

        var task = Task.Task.From(parentDto);

        if (compositeId.IsSoloId())
        {
            task.Update(request, DateTime.Now);
        }
        else
        {
            task.UpdateChildren(request, compositeId);
        }

        await _tasks.SetAsync(task.ToWriteDto());

        return parentDto.Id;
    }
}