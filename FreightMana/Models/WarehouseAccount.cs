using System;
using System.Collections.Generic;

namespace FreightMana.Models;

public partial class WarehouseAccount
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int WarehouseId { get; set; }

    public virtual Warehouse Warehouse { get; set; } = null!;
}
