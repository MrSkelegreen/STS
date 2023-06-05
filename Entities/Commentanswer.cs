using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Commentanswer
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int Author { get; set; }

    public DateOnly Answerdate { get; set; }

    public TimeOnly Answertime { get; set; }

    public virtual User AuthorNavigation { get; set; } = null!;

    public virtual ICollection<CtAnswerComment> CtAnswerComments { get; set; } = new List<CtAnswerComment>();
}
