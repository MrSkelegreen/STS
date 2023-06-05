using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Company
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly Startdate { get; set; }

    public int Owner { get; set; }

    public virtual User OwnerNavigation { get; set; } = null!;
}
