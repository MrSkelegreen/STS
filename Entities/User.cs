using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace STS.DAL.Entities;

public partial class User : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string? Aboutmyself { get; set; }

    public string? pw {  get; set; }

    /// <summary>
    /// false - applicant, true - employer
    /// </summary>
    public bool Role { get; set; }

    public virtual ICollection<Commentanswer> Commentanswers { get; set; } = new List<Commentanswer>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<Testgroup> Testgroups { get; set; } = new List<Testgroup>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

    private void OnPropertyChanged(string value)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(value));
        }
    }

}
