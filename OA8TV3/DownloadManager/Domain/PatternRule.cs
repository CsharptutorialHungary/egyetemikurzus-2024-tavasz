using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record PatternRule : AbstractRule, IComparable<AbstractRule>
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

        public override int CompareTo(AbstractRule? other)
        {
            if (other is PatternRule prule)
            {
                return String.Compare(Pattern, prule.Pattern, StringComparison.CurrentCulture);
            }
            return -1;
        }

        public override int GetHashCode()
        {
            return Pattern.GetHashCode();
        }
    }
}