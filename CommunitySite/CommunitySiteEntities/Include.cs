using System;
using System.Collections.Generic;

namespace CommunitySite.CommunitySiteEntities;

public partial class Include
{
    public decimal? Postid { get; set; }

    public decimal? Commentid { get; set; }

    public virtual Sitecomment? Comment { get; set; }

    public virtual Post? Post { get; set; }
}
