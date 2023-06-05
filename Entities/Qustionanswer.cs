using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Qustionanswer
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public bool Istrue { get; set; }

    public virtual ICollection<QuestionQuestionanswer> QuestionQuestionanswers { get; set; } = new List<QuestionQuestionanswer>();
}
