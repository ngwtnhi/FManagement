using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreightMana.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string? Product { get; set; }

    public int? NumberOfProduct { get; set; }

    public int TransportId { get; set; }

    public float? Cod { get; set; }

    public float? TransportFee { get; set; }

    public string? Note { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yy HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime? RecordAt { get; set; }

    public int ReceiverId { get; set; }

    public int SenderId { get; set; }

    public int WarehouseId { get; set; }

    public string? Status { get; set; }

    public int? CusId { get; set; }

    public DateTime? CancelAt { get; set; }
    public DateTime? CompleteAt { get; set; }
    public DateTime? ConfirmAt { get; set; }

    public virtual CusAccount? Cus { get; set; }

    public virtual Receiver Receiver { get; set; } = null!;

    public virtual Sender Sender { get; set; } = null!;

    public virtual Transport Transport { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
