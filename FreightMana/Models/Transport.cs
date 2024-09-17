using System;
using System.Collections.Generic;

namespace FreightMana.Models;

public partial class Transport
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public float? Cost { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
