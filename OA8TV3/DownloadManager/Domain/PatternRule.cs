using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record PatternRule : AbstractRule
    {
        [JsonPropertyName("pattern")]
        public required string Pattern { get; init; }
    }
}