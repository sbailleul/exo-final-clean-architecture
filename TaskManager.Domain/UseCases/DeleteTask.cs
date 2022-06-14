using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Task;
using TaskManager.Domain.Task.dtos;

namespace TaskManager.Domain.UseCases;

public class DeleteTask
{
    private readonly ITasks _tasks;

    public DeleteTask(ITasks tasks)
    {
        _tasks = tasks;
    }
    
    public async Task<int> Execute(DeleteTaskCommandDto request)
    {
        var compositeId = new CompleteParentId(request.CompleteId);
        var parentDto = await _tasks.FindOne(compositeId.GetFirstId());

        if (parentDto == null) throw new ParentNotFoundException();

        var task = Task.Task.From(parentDto);

        if (compositeId.IsSoloId())
        {
            _tasks.Delete(task.ToWriteDto());
            return task.Id.Value;
        }

        task.DeleteChildrenTask(compositeId);

        await _tasks.SetAsync(task.ToWriteDto());

        return parentDto.Id;
    }
}