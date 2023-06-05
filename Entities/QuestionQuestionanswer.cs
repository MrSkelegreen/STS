using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class QuestionQuestionanswer
{
    public int Id { get; set; }

    public int Questionid { get; set; }

    public int Questionanswerid { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Qustionanswer Questionanswer { get; set; } = null!;
}
