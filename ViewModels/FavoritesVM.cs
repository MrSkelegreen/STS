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
    internal class FavoritesVM : BaseViewModel
    {
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

        public FavoritesVM(User user)
        {
            Tests = new ObservableCollection<Test>() { };

            User = user;

            GetTests();
        }

        public void GetTests()
        {
            GetTestsCommand.Execute(null);
        }

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
                        
                         List<Favorite> favorites = context.Favorites.Include(f => f.Test.AuthorNavigation).Where(f => f.Owner == User.Id).ToList();      
                        
                        foreach (Favorite f in favorites)
                        {
                            tests.Add(f.Test);
                        }

                         //tests = context.Tests.Include(t => t.AuthorNavigation).Where(t => t.).OrderBy(t => t.Title).ToList();
                                  
                        
                        foreach (Test test in tests)
                        {
                            int authorId = test.Author;

                            Company company = context.Companies.FirstOrDefault(c => c.Owner == authorId);

                            if (company != null)
                            {
                                test.CompanyTitle = company.Title;
                            }

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
                            if (item.GetType() == typeof(FavoritesWindow))
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
                            if (item.GetType() == typeof(FavoritesWindow))
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
                            if (item.GetType() == typeof(FavoritesWindow))
                            {
                                item.Close();
                            }
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
                            if (item.GetType() == typeof(FavoritesWindow))
                            {
                                item.Close();
                            }
                        }

                    }));
            }
        }
    }
}
