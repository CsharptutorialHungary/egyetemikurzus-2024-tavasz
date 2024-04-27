using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entity;

public partial class Photo
{
    public decimal Photoid { get; set; }

    public decimal? Userid { get; set; }

    public string? PhotoType { get; set; }

    public decimal? PhotoSize { get; set; }

    public byte[]? PhotoInByte { get; set; }

    public string? PhotoName { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Siteuser? User { get; set; }
}
