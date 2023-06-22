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
using static System.Net.Mime.MediaTypeNames;

namespace STS.ViewModels
{
    internal class CompaniesVM : BaseViewModel
    {
        private ObservableCollection<Company> _companies;
        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set
            {
                _companies = value;
                OnPropertyChanged("Companies");
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
                OpenTestsOfCompaniesWindowCommand.Execute(null);
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

        public CompaniesVM(User user) 
        {
            Companies = new ObservableCollection<Company>();
            GetCompanies();
            User = user;
        }

        public void GetCompanies()
        {
            GetCompaniesCommand.Execute(null);
        }

        //Загрузка компаний
        private RelayCommand _getCompaniesCommand;

        public RelayCommand GetCompaniesCommand
        {
            get
            {
                return _getCompaniesCommand ??
                    (_getCompaniesCommand = new RelayCommand(get =>
                    {

                        var companies = new List<Company>();

                        STSContext context = new STSContext();

                        companies = context.Companies.Include(t => t.OwnerNavigation).OrderBy(t => t.Id).ToList();                       

                        foreach (Company company in companies)
                        {
                            Companies.Add(company);
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
                            if (item.GetType() == typeof(CompaniesListWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }


        private RelayCommand _openTestsOfCompaniesWindowCommand;
        public RelayCommand OpenTestsOfCompaniesWindowCommand
        {
            get
            {
                return _openTestsOfCompaniesWindowCommand ??
                    (_openTestsOfCompaniesWindowCommand = new RelayCommand(open =>
                    {
                        TestsOfCompaniesWindow tocw = new TestsOfCompaniesWindow();
                        tocw.DataContext = new TestsOfCompaniesVM(User, SelectedCompany);
                        tocw.Show();
                        foreach (Window item in App.Current.Windows)
                        {
                            if (item.GetType() == typeof(CompaniesListWindow))
                            {
                                item.Close();
                            }
                        }
                    }));
            }
        }

    }
}
