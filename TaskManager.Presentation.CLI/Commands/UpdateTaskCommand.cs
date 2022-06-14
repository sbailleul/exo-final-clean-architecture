using Spectre.Console;
using Spectre.Console.Cli;
using TaskManager.Domain.Task.dtos;
using TaskManager.Domain.UseCases;
using TaskManager.Infrastructure.Storage.contracts.Tasks;

namespace TaskManger.Presentation.CLI.Commands;

public class UpdateTaskSettings : CommandSettings
{
    [CommandArgument(0, "<ID>")] public string Id { get; set; }

    [CommandOption("-c|--content <CONTENT>")]
    public string? Content { get; set; }

    [CommandOption("-t|--tag <Tag>")] public string? Tag { get; set; }

    [CommandOption("-d|--due-date <DUE-DATE>")]
    public DateTime? DueDate { get; set; }

    [CommandOption("-s|--status <STATUS>")]
    public string? Status { get; set; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Id))
            return ValidationResult.Error($"{nameof(Id)} is required on add task Update");
        return base.Validate();
    }
}

public class UpdateTaskCommand : Command<UpdateTaskSettings>
{
    public override int Execute(CommandContext context, UpdateTaskSettings settings)
    {
        var taskUpdateDto = new UpdateTaskCommandDto(
            settings.Id,
            settings.Content,
            settings.Tag,
            settings.DueDate,
            settings.Status
        );

        return new UpdateTask(new TaskRepository())
            .Execute(taskUpdateDto).Result;

    }
}