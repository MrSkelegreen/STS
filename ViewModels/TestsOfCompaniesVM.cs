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
using System.Windows.Controls;

namespace STS.ViewModels
{
    internal class TestsOfCompaniesVM : BaseViewModel
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

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                OnPropertyChanged("SelectedCompany");
                
            }
        }

        private Test _selectedTest;
        public Test SelectedTest
        {
            get { return _selectedTest; }
            set
            {
                _selectedTest = value;
                OnPropertyChanged("SelectedTest");

                OpenTestCommand.Execute(_selectedTest);
            }
        }

        public TestsOfCompaniesVM(User user, Company company)
        {
            User = user;
            Tests = new ObservableCollection<Test>() { };
            SelectedCompany = company;
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
                        Tests.Clear();
                        var tests = new List<Test>();
                        STSContext context = new STSContext();
                        User user = context.Users.FirstOrDefault(u => u.Id == SelectedCompany.Owner);

                        if(user != null)
                        {
                            tests = context.Tests.Include(t => t.AuthorNavigation).OrderBy(t => t.Id).Where(t => t.Author == user.Id).ToList();                           
                            foreach (Test test in tests)
                            {
                                Favorite favoriteInDB = context.Favorites.FirstOrDefault(f => f.Owner == User.Id && f.Testid == test.Id);
                                if (favoriteInDB != null)
                                {
                                    test.BookmarkPath = "/Images/redBookmark.png";
                                }
                                else
                                {
                                    test.BookmarkPath = "/Images/bookMarkImage.png";
                                }

                                Tests.Add(test);
                            }
                            var comments = context.Tests.Include(t => t.TestComments).ToList();
                            var categories = context.Tests.Include(t => t.Category).ToList();
                        }
                        else
                        {
                            OpenApplicantWindowCommand.Execute(User);
                        }                   
                    }));
            }
        }

        private RelayCommand _addToFavoritesCommand;
        public RelayCommand AddToFavoritesCommand
        {
            get
            {
                return _addToFavoritesCommand ??
                    (_addToFavoritesCommand = new RelayCommand(a =>
                    {
                        STSContext context = new STSContext();
                        var selectedItem = (ListBoxItem)a;
                        Test test = selectedItem.Content as Test;
                        Favorite favoriteInDB = context.Favorites.FirstOrDefault(f => f.Owner == User.Id && f.Testid == test.Id);
                        if (favoriteInDB != null)
                        {
                            context.Favorites.Remove(favoriteInDB);
                        }
                        else
                        {
                            Favorite favorite = new Favorite() { Owner = User.Id, Testid = test.Id };
                            context.Favorites.Add(favorite);
                        }

                        context.SaveChanges();
                        GetTestsCommand.Execute(null);
                    }));
            }
        }

        private RelayCommand _openTestCommand;
        public RelayCommand OpenTestCommand
        {
            get
            {
                return _openTestCommand ??
                    (_openTestCommand = new RelayCommand(t =>
                    {
                        STSContext context = new STSContext();
                        var qs = context.Tests.Include(t => t.Questions).ToList();
                        TestWindow testWindow = new TestWindow(SelectedTest);
                        testWindow.DataContext = new TestVM(SelectedTest, User);
                        testWindow.Show();

                        foreach (Window item in App.Current.Windows)
                        {
                            if (item.GetType() == typeof(TestsOfCompaniesWindow))
                            {
                                item.Close();
                            }
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
                            if (item.GetType() == typeof(TestsOfCompaniesWindow))
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
                            if (item.GetType() == typeof(TestsOfCompaniesWindow))
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
                            if (item.GetType() == typeof(TestsOfCompaniesWindow))
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
                            if (item.GetType() == typeof(TestsOfCompaniesWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }

    }
}
