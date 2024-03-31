using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Friend
{
    public string? Friendemail1 { get; set; }

    public string? Friendemail2 { get; set; }

    public string? FriendStartDate { get; set; }

    public decimal? IsFriend { get; set; }

    public virtual Siteuser? Friendemail1Navigation { get; set; }

    public virtual Siteuser? Friendemail2Navigation { get; set; }
}
