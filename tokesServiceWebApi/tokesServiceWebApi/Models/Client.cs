using System;
using System.Collections.Generic;

namespace tokesServiceWebApi.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string? NameClient { get; set; }

    public string? EmailClient { get; set; }

    public string? PhoneClient { get; set; }

    public string? AddressClient { get; set; }

    public string? LastModification { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
