using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Sitegroup
{
    public decimal Groupid { get; set; }

    public string? Owneremail { get; set; }

    public string? Name { get; set; }

    public virtual Siteuser? OwneremailNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
