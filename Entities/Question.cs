using STS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace STS;

public partial class Question: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private int _id;
    public int Id
    {
        get {return _id; }
        set
        {
            _id = value;
            OnPropertyChanged("Id");
        } 
    }

    public string Content { get; set; } = null!;

    public string? Image { get; set; }

    public string Difficulty { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public string? Answer { get; set; }

    public int? Testid { get; set; }

    public virtual ICollection<QuestionComment> QuestionComments { get; set; } = new List<QuestionComment>();

    public virtual Test? Test { get; set; }

    //Добавил поле для проверки ответа
    [NotMapped] public string? UserAnswer { get; set; }

    //Добавил поле для нумерации вопросов

    [NotMapped] private int _localId;

    [NotMapped]
    public int LocalId
    {
        get { return _localId; }
        set
        {
            _localId = value;
            OnPropertyChanged("LocalId");
        }
    }

    private void OnPropertyChanged(string value)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(value));
        }
    }

}
