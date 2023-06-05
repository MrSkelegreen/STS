using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class QuestionComment
{
    public int Id { get; set; }

    public int Commentid { get; set; }

    public int Questionid { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
