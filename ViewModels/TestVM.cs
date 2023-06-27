using Microsoft.EntityFrameworkCore;
using STS.DAL.Entities;
using STS.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STS.ViewModels
{
    class TestVM : BaseViewModel
    {
        private Test _selectedTest;
        public Test SelectedTest
        {
            get { return _selectedTest; }
            set
            {
                _selectedTest = value;
                OnPropertyChanged("SelectedTest");
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

        private int _counter;
        public int Counter
        {
            get { return _counter; }
            set
            {
                _counter = value;
                OnPropertyChanged("Counter");
            }
        }

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

        public TestVM(Test test, User user)
        {
            SelectedTest = test;
            Questions = new ObservableCollection<Question>();
            LoadQuestions();
            Counter = 0;
            User = user;
        }

        private RelayCommand _getQuestions;
        public RelayCommand GetQuestions
        {
            get
            {
                return _getQuestions ??
                    (_getQuestions = new RelayCommand(t =>
                    {
                        STSContext context = new STSContext();
                        var categories = context.Tests.Include(t => t.Category).ToList();
                        var author = context.Tests.Include(t => t.AuthorNavigation).ToList();
                        var qs = context.Tests.Include(t => t.Questions).ToList();
                        var questions = new List<Question>();
                        questions = SelectedTest.Questions.OrderBy(q => q.Id).ToList();
                        var neededTest = context.Tests.FirstOrDefault(t => t.Id == SelectedTest.Id);                      
                        foreach (Question q in neededTest.Questions)
                        {
                            q.LocalId += 1;
                            Questions.Add(q);
                        }
                    }));
            }
        }

        public void LoadQuestions()
        {
            GetQuestions.Execute(null);
        }

        private RelayCommand _checkAnswersCommand;
        public RelayCommand CheckAnswersCommand
        {
            get
            {
                return _checkAnswersCommand ??
                    (_checkAnswersCommand = new RelayCommand(c =>
                    {
                        STSContext context = new STSContext();
                        Counter = 0;

                        for(int i = 0; i < Questions.Count; i++)
                        {
                            if (Questions[i].UserAnswer == Questions[i].Answer)
                            {
                                Counter++;
                            }
                        }

                        ResultWindow resultWindow = new ResultWindow(Counter.ToString());
                        resultWindow.Show();
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
                            if (item.GetType() == typeof(TestWindow))
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
                            if (item.GetType() == typeof(TestWindow))
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
                            if (item.GetType() == typeof(TestWindow))
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
                            if (item.GetType() == typeof(TestWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }
    }
}
