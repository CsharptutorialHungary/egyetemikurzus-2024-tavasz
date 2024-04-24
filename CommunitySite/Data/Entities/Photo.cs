namespace CommunitySite.Data.Entities;

public partial class Photo
{
    public int Photoid { get; set; }

    public int? Userid { get; set; }

    public string? PhotoType { get; set; }

    public string? PhotoUrl { get; set; }

    public int? PhotoSize { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Siteuser? User { get; set; }
}
