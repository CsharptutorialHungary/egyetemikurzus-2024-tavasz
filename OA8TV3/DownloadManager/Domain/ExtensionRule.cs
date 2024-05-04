using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record ExtensionRule : AbstractRule
    {
        [JsonPropertyName("extension")]
        public required string Extension { get; init; }
    }
}