using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    [JsonDerivedType(typeof(ExtensionRule), typeDiscriminator: "extension")]
    [JsonDerivedType(typeof(SizeRule), typeDiscriminator: "size")]
    [JsonDerivedType(typeof(PatternRule), typeDiscriminator: "pattern")]
    public abstract record AbstractRule
    {
        [JsonPropertyName("destination")]
        public required DestinationFolder Destination { get; init; }
    }
}