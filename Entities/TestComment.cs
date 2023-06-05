using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class TestComment
{
    public int Id { get; set; }

    public int Testid { get; set; }

    public int Commentid { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
