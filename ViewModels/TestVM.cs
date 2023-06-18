using Microsoft.EntityFrameworkCore;
using STS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public TestVM(Test test)
        {
            SelectedTest = test;
            STSContext context = new STSContext();

            var categories = context.Tests.Include(t => t.Category).ToList();
            var author = context.Tests.Include(t => t.AuthorNavigation).ToList();

            Questions = new ObservableCollection<Question>();

            //List<Question> questions = SelectedTest.TestQuestions.ToList();

            
        }        



    }
}
