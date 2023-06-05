using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int Author { get; set; }

    public DateOnly Commentdate { get; set; }

    public TimeOnly Commenttime { get; set; }

    public virtual User AuthorNavigation { get; set; } = null!;

    public virtual ICollection<CtAnswerComment> CtAnswerComments { get; set; } = new List<CtAnswerComment>();

    public virtual ICollection<QuestionComment> QuestionComments { get; set; } = new List<QuestionComment>();

    public virtual ICollection<TestComment> TestComments { get; set; } = new List<TestComment>();
}
