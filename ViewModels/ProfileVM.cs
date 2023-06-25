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
    internal class ProfileVM : BaseViewModel
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

        private string _stringRole;
        public string StringRole
        {
            get { return _stringRole; }
            set
            {
                _stringRole = value;
                OnPropertyChanged("StringRole");
            }
        }

        public ProfileVM(User user)
        {
            User = user;

            StringRole = User.Role ? "Работодатель" : "Соискатель";

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
                            if (item.GetType() == typeof(ProfileWindow))
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
                            if (item.GetType() == typeof(ProfileWindow))
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
                            if (item.GetType() == typeof(ProfileWindow))
                            {
                                item.Close();
                            }
                        }

                    }));
            }
        }

    }
}
