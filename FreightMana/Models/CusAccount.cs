using System;
using System.Collections.Generic;

namespace FreightMana.Models;

public partial class CusAccount
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
