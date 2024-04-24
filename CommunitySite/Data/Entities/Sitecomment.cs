using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Sitecomment
{
    public int Commentid { get; set; }

    public int? Postid { get; set; }

    public int? Userid { get; set; }

    public string? CommentText { get; set; }

    public string? CommentDate { get; set; }

    public virtual Post? Post { get; set; }

    public virtual Siteuser? User { get; set; }
}
