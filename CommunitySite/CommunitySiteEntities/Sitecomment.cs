using System;
using System.Collections.Generic;

namespace CommunitySite.CommunitySiteEntities;

public partial class Sitecomment
{
    public decimal Commentid { get; set; }

    public decimal? Postid { get; set; }

    public decimal? Userid { get; set; }

    public string? CommentText { get; set; }

    public string? CommentDate { get; set; }

    public virtual Post? Post { get; set; }

    public virtual Siteuser? User { get; set; }
}
