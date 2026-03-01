using DotLLM.HuggingFace;
using Xunit;

namespace DotLLM.Tests.Integration.Fixtures;

/// <summary>
/// Downloads a small GGUF model once and caches it for all tests in the collection.
/// Uses <c>QuantFactory/SmolLM-135M-GGUF</c> Q8_0 (~145 MB) — llama architecture,
/// so both GGUF parsing and <c>GgufModelConfigExtractor</c> work against it.
/// Cached in <c>~/.dotllm/test-cache/</c> across test runs.
/// </summary>
public sealed class SmallModelFixture : IAsyncLifetime
{
    private const string RepoId = "QuantFactory/SmolLM-135M-GGUF";
    private const string Filename = "SmolLM-135M.Q8_0.gguf";

    /// <summary>Full local path to the downloaded GGUF file.</summary>
    public string FilePath { get; private set; } = string.Empty;

    public async Task InitializeAsync()
    {
        string cacheDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".dotllm", "test-cache");

        string cachedPath = Path.Combine(cacheDir, RepoId.Replace('/', Path.DirectorySeparatorChar), Filename);

        if (File.Exists(cachedPath))
        {
            FilePath = cachedPath;
            return;
        }

        using var downloader = new HuggingFaceDownloader();
        FilePath = await downloader.DownloadFileAsync(RepoId, Filename, cacheDir);
    }

    public Task DisposeAsync() => Task.CompletedTask;
}

[CollectionDefinition("SmallModel")]
public class SmallModelCollection : ICollectionFixture<SmallModelFixture>;
