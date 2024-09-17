using System;
using System.Collections.Generic;

namespace FreightMana.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Hotline { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<WarehouseAccount> WarehouseAccounts { get; set; } = new List<WarehouseAccount>();
}
