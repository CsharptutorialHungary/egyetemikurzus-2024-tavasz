using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Photo
{
    public decimal Photoid { get; set; }

    public decimal? Userid { get; set; }

    public string? PhotoType { get; set; }

    public string? PhotoUrl { get; set; }

    public decimal? PhotoSize { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Siteuser? User { get; set; }
}
