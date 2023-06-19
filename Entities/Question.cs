using STS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace STS;

public partial class Question
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public string? Image { get; set; }

    //public int Typeid { get; set; }

    public string Difficulty { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public string? Answer { get; set; }

    public int? Testid { get; set; }

    public virtual ICollection<QuestionComment> QuestionComments { get; set; } = new List<QuestionComment>();

    public virtual Test? Test { get; set; }

    //Добавил поле для проверки ответа
    [NotMapped] public string? UserAnswer { get; set; }

}
