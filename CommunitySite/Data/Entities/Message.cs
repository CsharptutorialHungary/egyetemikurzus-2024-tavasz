using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Message
{
    public int Messageid { get; set; }

    public int? Senderid { get; set; }

    public int? Receiverid { get; set; }

    public string? MessageText { get; set; }

    public string? SendDate { get; set; }

    public virtual Siteuser? Receiver { get; set; }

    public virtual Siteuser? Sender { get; set; }
}
