using System;
using System.Collections.Generic;

namespace CommunitySite.CommunitySiteEntities;

public partial class Siteuser
{
    public decimal Userid { get; set; }

    public decimal? Permissionid { get; set; }

    public string? SurName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Passwords { get; set; }

    public string? Workplace { get; set; }

    public string? School { get; set; }

    public byte? BirthYear { get; set; }

    public byte? BirthMonth { get; set; }

    public byte? BirthDay { get; set; }

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual Permission? Permission { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Sitecomment> Sitecomments { get; set; } = new List<Sitecomment>();

    public virtual ICollection<Sitegroup> Sitegroups { get; set; } = new List<Sitegroup>();
}
