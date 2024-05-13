using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record SizeRule : AbstractRule, IComparable<AbstractRule>
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

        public override int CompareTo(AbstractRule? other)
        {
            if (other is SizeRule srule)
            {
                return ComparisonType == srule.ComparisonType ? 0 : ComparisonType < srule.ComparisonType ? -1 : 1;
            }
            return 1;
        }

        public override int GetHashCode()
        {
            return ComparisonType.GetHashCode();
        }
    }
}