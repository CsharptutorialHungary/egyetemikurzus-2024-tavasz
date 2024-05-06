using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record SizeRule : AbstractRule
    {
        [JsonPropertyName("comparisonType")]
        public required int ComparisonType { get; init; }
        [JsonPropertyName("value")]
        public required int Value { get; init; }

        public virtual bool Equals(SizeRule? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return ComparisonType == other.ComparisonType;
        }

        public override int GetHashCode()
        {
            return ComparisonType.GetHashCode();
        }
    }
}