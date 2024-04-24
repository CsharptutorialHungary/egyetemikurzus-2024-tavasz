using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Permission
{
    public int Permissionid { get; set; }

    public string? PermissionName { get; set; }

    public virtual ICollection<Siteuser> Siteusers { get; set; } = new List<Siteuser>();
}
