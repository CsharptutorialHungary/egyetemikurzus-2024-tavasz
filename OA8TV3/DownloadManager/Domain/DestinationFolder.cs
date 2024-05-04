using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record DestinationFolder
    {
        [JsonPropertyName("folderName")]
        public string? FolderName { get; init; }
        [JsonPropertyName("folderPath")]
        public required string FolderPath { get; init; }
    }
}