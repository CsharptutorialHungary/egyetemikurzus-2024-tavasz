using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Include
{
    public int? Postid { get; set; }

    public int? Commentid { get; set; }

    public virtual Sitecomment? Comment { get; set; }

    public virtual Post? Post { get; set; }
}
