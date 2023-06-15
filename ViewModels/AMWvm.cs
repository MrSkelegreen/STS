using STS.DAL.Entities;
using STS.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

//Applicant Main Window View Model

namespace STS.ViewModels
{
    class AMWvm : BaseViewModel
    {
        private ObservableCollection<Test> _tests;
        public ObservableCollection<Test> Tests
        {
            get { return _tests; }
            set 
            {
                _tests = value;
                OnPropertyChanged("Tests");
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

        private Uri _bookmarkUri;
        public Uri BookmarkUri
        {
            get { return _bookmarkUri; }
            set
            {
                _bookmarkUri = value;
                OnPropertyChanged("BookmarkUri");
            }
        }

        private int _itemTemplateFontSize;
        public int ItemTemplateFontSize
        {
            get { return _itemTemplateFontSize; }
            set
            {
                _itemTemplateFontSize = value;
                OnPropertyChanged("ItemTemplateFontSize");
            }
        }

        private IEnumerable<Test> _sortedTests;
        public IEnumerable<Test> SortedTests
        {
            get { return _sortedTests; }
            set
            {
                _sortedTests = value;
                OnPropertyChanged("SortedTests");
            }
        }

        public AMWvm(User user)
        {
            Tests = new ObservableCollection<Test>() {};

            //SortedTests = new IEnumerable<Test>() { };
           
            User = user;

            GetTests();

            _itemTemplateFontSize = 30;
        }

        public void GetTests()
        {
            GetTestsCommand.Execute(null);
        }

        //Загрузка тестов
        private RelayCommand _getTestsCommand;

        public RelayCommand GetTestsCommand
        {
            get
            {
                return _getTestsCommand ??
                    (_getTestsCommand = new RelayCommand(get =>
                    {
                                                                   
                        var tests = new List<Test>();

                        STSContext context = new STSContext();

                        tests = context.Tests.Include(t => t.AuthorNavigation).OrderBy(t => t.Id).ToList();

                        int commentsCount = 0;

                        foreach (Test test in tests)
                        {
                            Tests.Add(test);

                            
                        }
                        var comments = context.Tests.Include(t => t.TestComments).ToList();




                    }));
            }
        }

    }


}
