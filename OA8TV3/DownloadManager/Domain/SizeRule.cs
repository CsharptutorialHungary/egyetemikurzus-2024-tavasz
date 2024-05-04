using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record SizeRule : AbstractRule
    {
        [JsonPropertyName("comparisonType")]
        public required int ComparisonType { get; init; }
        [JsonPropertyName("value")]
        public required int Value { get; init; }
    }
}