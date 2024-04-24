using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Managegroup
{
    public int? Userid { get; set; }

    public int? Groupid { get; set; }

    public string? JoinDate { get; set; }

    public virtual Sitegroup? Group { get; set; }

    public virtual Siteuser? User { get; set; }
}
