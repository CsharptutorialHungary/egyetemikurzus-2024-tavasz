using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Friend
{
    public int? Friendid1 { get; set; }

    public int? Friendid2 { get; set; }

    public string? FriendStartDate { get; set; }

    public int? IsFriend { get; set; }

    public virtual Siteuser? Friendid1Navigation { get; set; }

    public virtual Siteuser? Friendid2Navigation { get; set; }
}
