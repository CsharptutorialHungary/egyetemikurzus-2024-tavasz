using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Siteuser
{
    public decimal? Permissionid { get; set; }

    public string? SurName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string? Passwords { get; set; }

    public string? Workplace { get; set; }

    public string? School { get; set; }

    public decimal? BirthYear { get; set; }

    public decimal? BirthMonth { get; set; }

    public decimal? BirthDay { get; set; }

    public virtual ICollection<Message> MessageReceiveremailNavigations { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenderemailNavigations { get; set; } = new List<Message>();

    public virtual Permission? Permission { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Sitecomment> Sitecomments { get; set; } = new List<Sitecomment>();

    public virtual ICollection<Sitegroup> Sitegroups { get; set; } = new List<Sitegroup>();
}
