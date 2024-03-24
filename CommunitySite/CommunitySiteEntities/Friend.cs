using System;
using System.Collections.Generic;

namespace CommunitySite.CommunitySiteEntities;

public partial class Friend
{
    public decimal? Friendid1 { get; set; }

    public decimal? Friendid2 { get; set; }

    public string? FriendStartDate { get; set; }

    public bool? IsFriend { get; set; }

    public virtual Siteuser? Friendid1Navigation { get; set; }

    public virtual Siteuser? Friendid2Navigation { get; set; }
}
