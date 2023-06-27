using Microsoft.EntityFrameworkCore;
using STS.DAL.Entities;
using STS.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STS.ViewModels
{
    class CreateTestVM : BaseViewModel 
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        private Test _developingTest;
        public Test DevelopingTest
        {
            get { return _developingTest; }
            set
            {
                _developingTest = value;
                OnPropertyChanged("DevelopingTest");
            }
        }

        private ObservableCollection<Question> _questions;
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;             
                OnPropertyChanged("Questions");                           
            }
        }

        private DateOnly _currentDate;
        public DateOnly CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                OnPropertyChanged("CurrentDate");
            }
        }

        private Question _selectedQuestion;
        public Question SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged("SelectedQuestion");
                if (value != null)
                {
                    IsDeleteBtnVisible = true;
                }
                else
                {
                    IsDeleteBtnVisible = false;
                }
            }
        }

        private bool _isDeleteBtnVisible;
        public bool IsDeleteBtnVisible
        {
            get { return _isDeleteBtnVisible; }
            set
            {
                _isDeleteBtnVisible = value;
                OnPropertyChanged("IsDeleteBtnVisible");
            }
        }

        private bool _isWarningVisible;
        public bool IsWarningVisible
        {
            get { return _isWarningVisible; }
            set
            {
                _isWarningVisible = value;
                OnPropertyChanged("IsWarningVisible");
            }
        }

        private string _warningText;
        public string WarningText
        {
            get { return _warningText; }
            set
            {
                _warningText = value;
                OnPropertyChanged("WarningText");
            }
        }

        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        private List<string> _difficulties;
        public List<string> Difficulties
        {
            get { return _difficulties; }
            set
            {
                _difficulties = value;
                OnPropertyChanged("Difficulties");
            }
        }

        private string _selectedDifficulty;
        public string SelectedDifficulty
        {
            get { return _selectedDifficulty; }
            set
            {
                _selectedDifficulty = value;
                OnPropertyChanged("SelectedDifficulty");
            }
        }

        public  CreateTestVM(User user)
        {
            User = user;
            DevelopingTest = new Test() { Author = user.Id, Title = string.Empty, Description = string.Empty};
            Questions = new ObservableCollection<Question>();
            CurrentDate = DateOnly.FromDateTime(DateTime.Now);
            IsWarningVisible = false;
            WarningText = string.Empty;
            STSContext context = new STSContext();
            Categories = context.Categories.ToList();          
            Difficulties = new List<string> { "Лёгкий", "Средний", "Сложный"};
            SelectedDifficulty = Difficulties[1];
        }
        //Добавить вопрос
        private RelayCommand _addQuestionCommand;
        public RelayCommand AddQuestionCommand
        {
            get
            {
                return _addQuestionCommand ??
                    (_addQuestionCommand = new RelayCommand(a =>
                    {
                        STSContext context = new STSContext();
                        Question q = new Question() { Content = string.Empty, Answer = string.Empty};
                        q.LocalId = Questions.Count + 1;                       
                        Questions.Add(q);
                    }));
            }
        }
        //Сохранить тест
        private RelayCommand _saveTestCommand;
        public RelayCommand SaveTestCommand
        {
            get
            {
                return _saveTestCommand ??
                    (_saveTestCommand = new RelayCommand(a =>
                    {
                        STSContext context = new STSContext();                                            
                        if (DevelopingTest.Title != "" && SelectedCategory.Id != 0)
                        {                          
                            if (Questions.Count > 0)
                            {
                                bool questionsFilled = true; 
                                foreach (Question q in Questions)
                                {
                                    if (q.Content == "" || q.Answer == "")
                                    {
                                        IsWarningVisible = true;
                                        WarningText = "Ошибка: Заполнены не все вопросы";
                                        questionsFilled = false;
                                        break;
                                    }                                   
                                }
                                if (questionsFilled)
                                {                                    
                                    IsWarningVisible = false;
                                    DevelopingTest.Categoryid = SelectedCategory.Id;
                                    DevelopingTest.Creationdate = CurrentDate;
                                    DevelopingTest.Difficulty = SelectedDifficulty;                                                                                                       
                                    DevelopingTest.Questions = Questions;
                                    context.Tests.Add(DevelopingTest);
                                    context.SaveChanges();
                                    OpenApplicantWindowCommand.Execute(User);
                                }
                            }
                            else
                            {
                                IsWarningVisible = true;
                                WarningText = "Ошибка: В тесте отсутствуют вопросы";
                            }
                        }
                        else
                        {
                            IsWarningVisible = true;
                            WarningText = "Ошибка: Укажите название теста и категорию";
                        }                    
                    }));
            }
        }
        //Удалить вопрос
        private RelayCommand _deleteQuestionCommand;
        public RelayCommand DeleteQuestionCommand
        {
            get
            {
                return _deleteQuestionCommand ??
                    (_deleteQuestionCommand = new RelayCommand(d =>
                    {
                        STSContext context = new STSContext();                   
                        if(SelectedQuestion != null)
                        {
                            _questions.Remove(SelectedQuestion);
                            for (int i = 1; i < Questions.Count + 1; i++)
                            {
                                Questions[i - 1].LocalId = i;
                            }
                            OnPropertyChanged("Questions");
                        }                      
                    }));
            }
        }

        private RelayCommand _openApplicantWindowCommand;
        public RelayCommand OpenApplicantWindowCommand
        {
            get
            {
                return _openApplicantWindowCommand ??
                    (_openApplicantWindowCommand = new RelayCommand(open =>
                    {
                        ApplicantMainWindow amw = new ApplicantMainWindow();
                        amw.DataContext = new AMWvm(User);
                        amw.Show();
                        foreach (Window item in App.Current.Windows)
                        {
                            if (item.GetType() == typeof(CreateTestWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }

        private RelayCommand _openCompaniesListWindowCommand;
        public RelayCommand OpenCompaniesListWindowCommand
        {
            get
            {
                return _openCompaniesListWindowCommand ??
                    (_openCompaniesListWindowCommand = new RelayCommand(o =>
                    {
                        STSContext context = new STSContext();
                        CompaniesListWindow clw = new CompaniesListWindow();
                        clw.DataContext = new CompaniesVM(User);
                        clw.Show();
                        foreach (Window item in App.Current.Windows)
                        {
                            if (item.GetType() == typeof(CreateTestWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }

        private RelayCommand _openFavoritesWindowComand;
        public RelayCommand OpenFavoritesWindowComand
        {
            get
            {
                return _openFavoritesWindowComand ??
                    (_openFavoritesWindowComand = new RelayCommand(o =>
                    {
                        STSContext context = new STSContext();
                        FavoritesWindow favoritesWindow = new FavoritesWindow();
                        favoritesWindow.DataContext = new FavoritesVM(User);
                        favoritesWindow.Show();
                        foreach (Window item in App.Current.Windows)
                        {
                            if (item.GetType() == typeof(CreateTestWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }

        private RelayCommand _openProfileWindowCommand;
        public RelayCommand OpenProfileWindowCommand
        {
            get
            {
                return _openProfileWindowCommand ??
                    (_openProfileWindowCommand = new RelayCommand(o =>
                    {
                        STSContext context = new STSContext();
                        ProfileWindow profileWindow = new ProfileWindow();
                        profileWindow.DataContext = new ProfileVM(User);
                        profileWindow.Show();
                        foreach (Window item in App.Current.Windows)
                        {
                            if (item.GetType() == typeof(CreateTestWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }
    }
}
