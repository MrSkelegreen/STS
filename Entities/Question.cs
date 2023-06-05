using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Question
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public string? Image { get; set; }

    public int Typeid { get; set; }

    public string Difficulty { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public virtual ICollection<QuestionComment> QuestionComments { get; set; } = new List<QuestionComment>();

    public virtual ICollection<QuestionQuestionanswer> QuestionQuestionanswers { get; set; } = new List<QuestionQuestionanswer>();

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    public virtual Questiontype Type { get; set; } = null!;
}
