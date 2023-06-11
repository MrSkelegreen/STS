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

        public AMWvm(User user)
        {
            Tests = new ObservableCollection<Test>() {};
           
            User = user;

            GetTests();
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

                        tests = context.Tests.ToList();

                        foreach (Test test in tests)
                        {
                            Tests.Add(test);
                        }

                    }));
            }
        }

    }


}
