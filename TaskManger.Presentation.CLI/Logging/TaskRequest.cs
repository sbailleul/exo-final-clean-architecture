namespace TaskManger.Presentation.CLI.Logging;

public record TaskRequest<TInnerRequest>(string OriginalCommand, TInnerRequest InnerRequest);