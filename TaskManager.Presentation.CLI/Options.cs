using CommandLine;

namespace TaskManger.Presentation.CLI;

public class Options
{
    [Option('o', "output", Required = false,
        HelpText = "Output directory for generated class definitions, default is current directory")]
    public string? OutputDirectory { get; set; }

    [Option('f', "file", Required = true, HelpText = "Source file for class generation, process e.g : file.yaml")]
    public string File { get; set; } = null!;
}