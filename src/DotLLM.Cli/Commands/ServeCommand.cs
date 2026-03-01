using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DotLLM.Cli.Commands;

/// <summary>
/// Stub command for launching the OpenAI-compatible API server.
/// </summary>
internal sealed class ServeCommand : Command<ServeCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandOption("--port")]
        [Description("Port to listen on.")]
        [DefaultValue(8080)]
        public int Port { get; set; } = 8080;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.MarkupLine("[yellow]The 'serve' command is not yet implemented.[/]");
        AnsiConsole.MarkupLine($"Would start server on port [bold]{settings.Port}[/].");
        return 0;
    }
}
