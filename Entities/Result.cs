using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Result
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public int Testid { get; set; }

    public int Score { get; set; }

    public virtual Test Test { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
