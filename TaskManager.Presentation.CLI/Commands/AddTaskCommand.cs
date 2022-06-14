using Spectre.Console;
using Spectre.Console.Cli;
using TaskManager.Domain.Task.dtos;
using TaskManager.Domain.UseCases;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Storage.contracts.Tasks;

namespace TaskManger.Presentation.CLI.Commands;

public class AddTaskSettings : CommandSettings
{
    [CommandOption("-c|--content <CONTENT>")]
    public string Content { get; set; }

    [CommandOption("-t|--tag <TAG>")] public string? Tag { get; set; }

    [CommandOption("-p|--parent-id <PARENT-ID>")]
    public string? ParentTaskId { get; set; }

    [CommandOption("-d|--due-date <DUE-DATE>")]
    public DateTime? DueDate { get; set; }

    [CommandOption("-s|--status <STATUS>")]
    public string? Status { get; set; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Content)) return ValidationResult.Error($"{nameof(Content)} is required on add task Action");
        return base.Validate();
    }
}

public class AddTaskCommand : Command<AddTaskSettings>
{
    public override int Execute(CommandContext context, AddTaskSettings settings)
    {

        if (settings.ParentTaskId is null)
        {
            var taskCreationDto = new CreateTaskCommandDto(
                settings.Content,
                settings.Tag,
                settings.DueDate,
                settings.Status
            );

            return new CreateTask(new TaskRepository(), new TimeGenerator())
                .Execute(taskCreationDto).Result;

        }

        var subTaskCreationDto = new AddSubTaskCommandDto(
            settings.Content,
            settings.Tag,
            settings.DueDate,
            settings.ParentTaskId,
            settings.Status
        );

        return new AddSubTask(new TaskRepository(), new TimeGenerator())
            .Execute(subTaskCreationDto).Result;
    }
}