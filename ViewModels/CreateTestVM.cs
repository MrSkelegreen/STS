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

        public  CreateTestVM(User user)
        {
            User = user;
            Questions = new ObservableCollection<Question>();
            CurrentDate = DateOnly.FromDateTime(DateTime.Now);
        }

        private RelayCommand _addQuestionCommand;
        public RelayCommand AddQuestionCommand
        {
            get
            {
                return _addQuestionCommand ??
                    (_addQuestionCommand = new RelayCommand(a =>
                    {

                        STSContext context = new STSContext();

                        Question q = new Question();
                        Questions.Add(q);

                    }));
            }
        }

        private RelayCommand _saveTestCommand;
        public RelayCommand SaveTestCommand
        {
            get
            {
                return _saveTestCommand ??
                    (_saveTestCommand = new RelayCommand(a =>
                    {

                        STSContext context = new STSContext();

                        

                    }));
            }
        }
    }
}
