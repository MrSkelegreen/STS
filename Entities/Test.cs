using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Test
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int Categoryid { get; set; }

    public string Difficulty { get; set; } = null!;

    public string? Image { get; set; }

    public DateOnly Creationdate { get; set; }

    public double Rating { get; set; }

    public int Author { get; set; }

    public virtual User AuthorNavigation { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<TestComment> TestComments { get; set; } = new List<TestComment>();

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    public virtual ICollection<TestTestgroup> TestTestgroups { get; set; } = new List<TestTestgroup>();
}
