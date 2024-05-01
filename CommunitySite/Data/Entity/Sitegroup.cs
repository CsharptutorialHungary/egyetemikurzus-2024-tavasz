using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entity;

public partial class Sitegroup
{
    public decimal Groupid { get; set; }

    public decimal? Ownerid { get; set; }

    public string? Name { get; set; }

    public Guid Grouptechnicalname { get; set; }

    public virtual ICollection<Managegroup> Managegroups { get; set; } = new List<Managegroup>();

    public virtual Siteuser? Owner { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
