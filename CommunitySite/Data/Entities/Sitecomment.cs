using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Sitecomment
{
    public decimal Commentid { get; set; }

    public decimal? Postid { get; set; }

    public string? Email { get; set; }

    public string? CommentText { get; set; }

    public string? CommentDate { get; set; }

    public virtual Siteuser? EmailNavigation { get; set; }

    public virtual Post? Post { get; set; }
}
