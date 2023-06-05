using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Questiontype
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
