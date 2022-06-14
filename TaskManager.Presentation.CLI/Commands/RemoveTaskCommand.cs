using Spectre.Console;
using Spectre.Console.Cli;
using TaskManager.Domain.Task.dtos;
using TaskManager.Domain.UseCases;
using TaskManager.Infrastructure.Storage.contracts.Tasks;

namespace TaskManger.Presentation.CLI.Commands;

public class RemoveTaskSettings : CommandSettings
{
    [CommandArgument(0, "<ID>")] public string Id { get; set; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Id)) return ValidationResult.Error($"{nameof(Id)} is required on remove task Action");
        return base.Validate();
    }
}

public class RemoveTaskCommand : Command<RemoveTaskSettings>
{
    public override int Execute(CommandContext context, RemoveTaskSettings settings)
    {
        var removeTaskDto = new DeleteTaskCommandDto(settings.Id);

        return new DeleteTask(new TaskRepository()).Execute(removeTaskDto).Result;
    }
}