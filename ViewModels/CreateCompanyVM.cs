using STS.DAL.Entities;
using STS.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STS.ViewModels
{
    class CreateCompanyVM : BaseViewModel
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

        private Company _newCompany;
        public Company NewCompany
        {
            get { return _newCompany; }
            set
            {
                _newCompany = value;
                OnPropertyChanged("NewCompany");
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private bool _isWarningVisible;
        public bool IsWarningVisible
        {
            get { return _isWarningVisible; }
            set
            {
                _isWarningVisible = value;
                OnPropertyChanged("IsWarningVisible");
            }
        }

        public CreateCompanyVM(User user) 
        {
            User = user;           

            SelectedDate = DateTime.Now;

            NewCompany = new Company()
            {
                Title = string.Empty,
                Description = string.Empty,
                Owner = User.Id,
                //Startdate = DateOnly.FromDateTime(DateTime.Now)
            };

            IsWarningVisible = false;

        }

        private RelayCommand _addCompanyCommand;
        public RelayCommand AddCompanyCommand
        {
            get
            {
                return _addCompanyCommand ??
                    (_addCompanyCommand = new RelayCommand(open =>
                    {
                        
                        if (NewCompany.Title != "")
                        {
                            NewCompany.Startdate = DateOnly.FromDateTime(SelectedDate);

                            STSContext context = new STSContext();

                            context.Companies.Add(NewCompany);

                            context.SaveChanges();

                            

                            foreach (Window item in App.Current.Windows)
                            {
                                if (item.GetType() == typeof(CompaniesListWindow))
                                {
                                    item.Close();                                   
                                }
                            }

                            CompaniesListWindow clw = new CompaniesListWindow();
                            clw.DataContext = new CompaniesVM(User);
                            clw.Show();

                            foreach (Window item in App.Current.Windows)
                            {
                                if (item.GetType() == typeof(CreateCompanyWindow))
                                {
                                    item.Close();
                                }
                            }

                        }
                        else
                        {
                            IsWarningVisible = true;
                        }
                      
                    }));
            }
        }
    }
}
