using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Photo
{
    public decimal Photoid { get; set; }

    public string? Email { get; set; }

    public string? PhotoType { get; set; }

    public string? PhotoUrl { get; set; }

    public decimal? PhotoSize { get; set; }

    public virtual Siteuser? EmailNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
