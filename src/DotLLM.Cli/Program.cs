using DotLLM.Cli.Commands;
#if DEBUG
using DotLLM.Cli.Commands.Debug;
#endif
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.SetApplicationName("dotllm");
    config.SetApplicationVersion("0.1.0");

    config.AddBranch("model", model =>
    {
        model.SetDescription("Manage GGUF models from HuggingFace Hub.");

        model.AddCommand<ModelSearchCommand>("search")
            .WithDescription("Search HuggingFace for GGUF models.")
            .WithExample("model", "search", "llama", "--limit", "5");

        model.AddCommand<ModelPullCommand>("pull")
            .WithDescription("Download a GGUF model from HuggingFace.")
            .WithExample("model", "pull", "TheBloke/Llama-2-7B-GGUF", "--file", "llama-2-7b.Q4_K_M.gguf");

        model.AddCommand<ModelListCommand>("list")
            .WithDescription("List locally downloaded models.");

        model.AddCommand<ModelInfoCommand>("info")
            .WithDescription("Show details for a HuggingFace model repository.")
            .WithExample("model", "info", "TheBloke/Llama-2-7B-GGUF");
    });

    config.AddCommand<RunCommand>("run")
        .WithDescription("Run inference on a GGUF model (stub).")
        .WithExample("run", "~/.dotllm/models/model.gguf", "--prompt", "Hello");

    config.AddCommand<ServeCommand>("serve")
        .WithDescription("Launch OpenAI-compatible API server (stub).")
        .WithExample("serve", "--port", "8080");

#if DEBUG
    config.AddBranch("debug", debug =>
    {
        debug.SetDescription("Debug/diagnostic commands (Debug build only).");

        debug.AddCommand<DebugGgufHeaderCommand>("gguf-header")
            .WithDescription("Parse and display the GGUF file header.")
            .WithExample("debug", "gguf-header", "model.gguf");

        debug.AddCommand<DebugGgufMetadataCommand>("gguf-metadata")
            .WithDescription("List all GGUF metadata key-value pairs.")
            .WithExample("debug", "gguf-metadata", "model.gguf");

        debug.AddCommand<DebugGgufTensorsCommand>("gguf-tensors")
            .WithDescription("List all tensor descriptors from a GGUF file.")
            .WithExample("debug", "gguf-tensors", "model.gguf");

        debug.AddCommand<DebugGgufConfigCommand>("gguf-config")
            .WithDescription("Extract and display ModelConfig from GGUF metadata.")
            .WithExample("debug", "gguf-config", "model.gguf");
    });
#endif
});

return app.Run(args);
