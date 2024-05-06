using System.Text.Json.Serialization;

namespace DownloadManager.Domain
{
    public record ExtensionRule : AbstractRule
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
            return Extension == other.Extension;
        }

        public override int GetHashCode()
        {
            return Extension.GetHashCode();
        }
    }
}