using System;
using System.Collections.Generic;

namespace STS.DAL.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
