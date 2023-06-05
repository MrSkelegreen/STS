using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class CtAnswerComment
{
    public int Id { get; set; }

    public int Commentid { get; set; }

    public int Commentanswerid { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual Commentanswer Commentanswer { get; set; } = null!;
}
