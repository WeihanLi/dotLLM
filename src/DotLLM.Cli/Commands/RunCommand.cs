using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DotLLM.Cli.Commands;

/// <summary>
/// Stub command for running inference on a GGUF model.
/// </summary>
internal sealed class RunCommand : Command<RunCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "<model-path>")]
        [Description("Path to the GGUF model file.")]
        public string ModelPath { get; set; } = string.Empty;

        [CommandOption("--prompt|-p")]
        [Description("Input prompt for generation.")]
        public string? Prompt { get; set; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.MarkupLine("[yellow]The 'run' command is not yet implemented.[/]");
        AnsiConsole.MarkupLine($"Model: [bold]{settings.ModelPath.EscapeMarkup()}[/]");
        if (!string.IsNullOrEmpty(settings.Prompt))
            AnsiConsole.MarkupLine($"Prompt: {settings.Prompt.EscapeMarkup()}");
        return 0;
    }
}
