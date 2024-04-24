using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Sitegroup
{
    public int Groupid { get; set; }

    public int? Ownerid { get; set; }

    public string? Name { get; set; }

    public virtual Siteuser? Owner { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
