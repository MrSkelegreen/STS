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

        private List<string> _sortings;
        public List<string> Sortings
        {
            get { return _sortings; }
            set
            {
                _sortings = value;
                OnPropertyChanged("Sortings");
            }
        }

        private string _selectedSorting;
        public string SelectedSorting
        {
            get { return _selectedSorting; }
            set
            {
                _selectedSorting = value;
                OnPropertyChanged("SelectedSorting");
                Tests.Clear();
                GetTests();
            }
        }

        private List<string> _subsortings;
        public List<string> Subsortings
        {
            get { return _subsortings; }
            set
            {
                _subsortings = value;
                OnPropertyChanged("Subsortings");
            }
        }

        private string _selectedSuborting;
        public string SelectedSubsorting
        {
            get { return _selectedSuborting; }
            set
            {
                _selectedSuborting = value;
                OnPropertyChanged("SelectedSubsorting");
                Tests.Clear();
                GetTests();
            }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                OnPropertyChanged("SearchString");               
            }
        }

        private bool _isCreateTestButtonVisible;
        public bool IsCreateTestButtonVisible
        {
            get { return _isCreateTestButtonVisible; }
            set
            {
                _isCreateTestButtonVisible = value;
                OnPropertyChanged("IsCreateTestButtonVisible");              
            }
        }

        public AMWvm(User user)
        {
            Tests = new ObservableCollection<Test>() {};           
            User = user;
            SearchString = string.Empty;

            Sortings = new List<string>()
            {
                "По названию",
                "По категории",
                "По автору"
            };

            Subsortings = new List<string>()
            {
                "По возрастанию",
                "По убыванию"
            };

            SelectedSorting = Sortings[0];
            SelectedSubsorting = Subsortings[0];
            IsCreateTestButtonVisible = User.Role;
            GetTests();    
        }

        public void GetTests()
        {
            GetTestsCommand.Execute(SearchString);
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
                       
                        string ss = get.ToString();
                        if (ss != null)
                        {
                            SearchString = ss;
                        }
                        
                        if(SearchString != "")
                        {
                            switch (SelectedSorting)
                            {
                                case "По названию":
                                    if (SelectedSubsorting == "По возрастанию")
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).Where(t =>EF.Functions.ILike(t.Title,$"%{SearchString}%")).OrderBy(t => t.Title).ToList();
                                    }
                                    else
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).Where(t => EF.Functions.ILike(t.Title, $"%{SearchString}%")).OrderByDescending(t => t.Title).ToList();
                                    }
                                    break;

                                case "По категории":
                                    if (SelectedSubsorting == "По возрастанию")
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).Where(t => EF.Functions.ILike(t.Title, $"%{SearchString}%")).OrderBy(t => t.Category.Title).ToList();
                                    }
                                    else
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).Where(t => EF.Functions.ILike(t.Title, $"%{SearchString}%")).OrderByDescending(t => t.Category.Title).ToList();
                                    }
                                    break;

                                case "По автору":
                                    if (SelectedSubsorting == "По возрастанию")
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).Where(t => EF.Functions.ILike(t.Title, $"%{SearchString}%")).OrderBy(t => t.AuthorNavigation.Lastname).ToList();
                                    }
                                    else
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).Where(t => EF.Functions.ILike(t.Title, $"%{SearchString}%")).OrderByDescending(t => t.AuthorNavigation.Lastname).ToList();
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (SelectedSorting)
                            {
                                case "По названию":
                                    if (SelectedSubsorting == "По возрастанию")
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).OrderBy(t => t.Title).ToList();
                                    }
                                    else
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).OrderByDescending(t => t.Title).ToList();
                                    }
                                    break;

                                case "По категории":
                                    if (SelectedSubsorting == "По возрастанию")
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).OrderBy(t => t.Category.Title).ToList();
                                    }
                                    else
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).OrderByDescending(t => t.Category.Title).ToList();
                                    }
                                    break;

                                case "По автору":
                                    if (SelectedSubsorting == "По возрастанию")
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).OrderBy(t => t.AuthorNavigation.Lastname).ToList();
                                    }
                                    else
                                    {
                                        tests = context.Tests.Include(t => t.AuthorNavigation).OrderByDescending(t => t.AuthorNavigation.Lastname).ToList();
                                    }
                                    break;
                            }
                        }
                                                                 
                        foreach (Test test in tests)
                        {
                            int authorId = test.Author;

                            Company company = context.Companies.FirstOrDefault(c => c.Owner == authorId);

                            if(company != null)
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

        //Добавление в избранное
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
                        GetTestsCommand.Execute(SearchString);
                    }));
            }
        }
        //Открытие теста
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
                            if (item.GetType() == typeof(ApplicantMainWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }
        //Открытие окна создания теста
        private RelayCommand _openCreateTestWindowCommand;
        public RelayCommand OpenCreateTestWindowCommand
        {
            get
            {
                return _openCreateTestWindowCommand ??
                    (_openCreateTestWindowCommand = new RelayCommand(o =>
                    {
                        STSContext context = new STSContext();
                        CreateTestWindow ctw = new CreateTestWindow();
                        ctw.DataContext = new CreateTestVM(User);
                        ctw.Show();

                        foreach (Window item in App.Current.Windows)
                        {
                            if (item.GetType() == typeof(ApplicantMainWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }
        //Открытие окна со списком компаний
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
                            if (item.GetType() == typeof(ApplicantMainWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }
        //Открытие окна избранного
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
                            if (item.GetType() == typeof(ApplicantMainWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }
        //Открытие окна профиля
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
                            if (item.GetType() == typeof(ApplicantMainWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }    
    }
}
