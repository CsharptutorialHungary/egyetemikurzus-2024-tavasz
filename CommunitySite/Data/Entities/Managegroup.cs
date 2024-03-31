using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Managegroup
{
    public string? Email { get; set; }

    public decimal? Groupid { get; set; }

    public string? JoinDate { get; set; }

    public virtual Siteuser? EmailNavigation { get; set; }

    public virtual Sitegroup? Group { get; set; }
}
