using System;
using System.Collections.Generic;

namespace FreightMana.Models;

public partial class Staff
{
    public int Id { get; set; }


    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Position { get; set; }

    public int WarehouseId { get; set; }

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual Warehouse Warehouse { get; set; } = null!;
}
