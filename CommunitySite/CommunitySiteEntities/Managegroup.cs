﻿using System;
using System.Collections.Generic;

namespace CommunitySite.CommunitySiteEntities;

public partial class Managegroup
{
    public decimal? Userid { get; set; }

    public decimal? Groupid { get; set; }

    public string? JoinDate { get; set; }

    public virtual Sitegroup? Group { get; set; }

    public virtual Siteuser? User { get; set; }
}
