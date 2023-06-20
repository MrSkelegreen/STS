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
            // STSContext context = new STSContext();
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
    }
}
