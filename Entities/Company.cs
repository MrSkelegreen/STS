using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace STS.DAL.Entities;

public partial class Company : INotifyPropertyChanged
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    private DateOnly _startdate;
    public DateOnly Startdate
    {
        get { return _startdate; }
        set
        {
            _startdate = value;
            OnPropertyChanged("Startdate");
        }
    }

    public int Owner { get; set; }

    public virtual User OwnerNavigation { get; set; } = null!;

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string value)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(value));
        }
    }

}
