using System;
using System.Collections.Generic;

namespace CommunitySite.CommunitySiteEntities;

public partial class Message
{
    public decimal Messageid { get; set; }

    public decimal? Senderid { get; set; }

    public decimal? Receiverid { get; set; }

    public string? MessageText { get; set; }

    public string? SendDate { get; set; }

    public virtual Siteuser? Receiver { get; set; }

    public virtual Siteuser? Sender { get; set; }
}
