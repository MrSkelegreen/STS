using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class TestQuestion
{
    public int Id { get; set; }

    public int Testid { get; set; }

    public int Questionid { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
