using System;
using System.Collections.Generic;

namespace CommunitySite.Data.Entities;

public partial class Message
{
    public decimal Messageid { get; set; }

    public string? Senderemail { get; set; }

    public string? Receiveremail { get; set; }

    public string? MessageText { get; set; }

    public string? SendDate { get; set; }

    public virtual Siteuser? ReceiveremailNavigation { get; set; }

    public virtual Siteuser? SenderemailNavigation { get; set; }
}
