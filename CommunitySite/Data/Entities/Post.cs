using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Post
{
    public int Postid { get; set; }

    public int? Userid { get; set; }

    public int? Photoid { get; set; }

    public int? Groupid { get; set; }

    public string? PostText { get; set; }

    public string? PostDate { get; set; }

    public virtual Sitegroup? Group { get; set; }

    public virtual Photo? Photo { get; set; }

    public virtual ICollection<Sitecomment> Sitecomments { get; set; } = new List<Sitecomment>();

    public virtual Siteuser? User { get; set; }
}
