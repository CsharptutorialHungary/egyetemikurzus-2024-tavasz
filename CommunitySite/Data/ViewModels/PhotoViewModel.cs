#nullable enable

using CommunitySite.Data.Entity;

namespace CommunitySite.Data.ViewModels
{
    public class PhotoViewModel
    {
        public decimal Photoid { get; set; }

        public decimal? Userid { get; set; }

        public string? PhotoType { get; set; }

        public decimal? PhotoSize { get; set; }

        public byte[]? PhotoInByte { get; set; }

        public string? PhotoName { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public Siteuser? User { get; set; }
    }
}
