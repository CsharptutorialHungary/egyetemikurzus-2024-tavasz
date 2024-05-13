using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record ExtensionRule : AbstractRule, IComparable<AbstractRule>
    {
        [JsonPropertyName("extension")]
        public required string Extension { get; init; }

        public virtual bool Equals(ExtensionRule? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return String.Equals(Extension, other.Extension, StringComparison.CurrentCultureIgnoreCase);
        }

        public override int CompareTo(AbstractRule? other)
        {
            if (other is ExtensionRule erule)
            {
                return String.Compare(Extension, erule.Extension, StringComparison.CurrentCultureIgnoreCase);
            }
            if (other is PatternRule)
            {
                return 1;
            }
            return -1;
        }

        public override int GetHashCode()
        {
            return Extension.GetHashCode();
        }
    }
}