using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Favorite
{
    public int Id { get; set; }

    public int Owner { get; set; }

    public int Testid { get; set; }

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
