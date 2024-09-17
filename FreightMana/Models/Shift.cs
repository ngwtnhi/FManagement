using System;
using System.Collections.Generic;

namespace FreightMana.Models;

public partial class Shift
{
    public int Id { get; set; }

    public TimeOnly? TimeStart { get; set; }

    public TimeOnly? TimeEnd { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? Day { get; set; }

    public virtual Staff? Employee { get; set; }
}
