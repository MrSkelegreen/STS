using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class TestTestgroup
{
    public int Id { get; set; }

    public int Testgroupid { get; set; }

    public int? Testid { get; set; }

    public virtual Test? Test { get; set; }

    public virtual Testgroup Testgroup { get; set; } = null!;
}
