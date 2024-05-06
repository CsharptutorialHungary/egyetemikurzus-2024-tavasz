using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record PatternRule : AbstractRule
    {
        [JsonPropertyName("pattern")]
        public required string Pattern { get; init; }

        public virtual bool Equals(PatternRule? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Pattern == other.Pattern;
        }

        public override int GetHashCode()
        {
            return Pattern.GetHashCode();
        }
    }
}