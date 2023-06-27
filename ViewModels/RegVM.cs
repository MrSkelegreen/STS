using STS.DAL.Entities;
using STS.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace STS.ViewModels
{
    class RegVM : BaseViewModel
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

        private string _warningText;
        public string WarningText
        {
            get { return _warningText; }
            set
            {
                _warningText = value;
                OnPropertyChanged("WarningText");
            }
        }

        private ObservableCollection<string> _roles;
        public ObservableCollection<string> Roles
        {
            get { return _roles; }
            set 
            {
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }

        private string _selectedRole;
        public string SelectedRole
        {
            get { return _selectedRole; }
            set 
            {
                _selectedRole = value;
                OnPropertyChanged("SelectedRole");
            }
        }

        public RegVM()
        {
            _isWarningVisible = false;
            STSContext context = new STSContext();
            _user = new User();
            _user.Email = string.Empty;
            _user.pw = string.Empty;           

            Roles = new ObservableCollection<string>()
            {
                "Соискатель",
                "Работодатель"
            };

            SelectedRole = Roles[0];
            _warningText= "";
        }
        //Регистрация
        private RelayCommand _regCommand;
        public RelayCommand RegCommand
        {
            get 
            {
                return _regCommand ??
                   (_regCommand = new RelayCommand(password =>
                   {
                       _user.pw = ((PasswordBox)password).Password;
                       STSContext context = new STSContext();
                       var user = context.Users.SingleOrDefault(x => x.Email == _user.Email);
                       if(_user.Email == string.Empty || _user.pw == string.Empty || _user.Firstname == string.Empty || _user.Lastname == string.Empty)
                       {
                           WarningText = "Введены некорректные данные";
                           IsWarningVisible = true;
                       }
                       else
                       {
                           if (user == null)
                           {
                                                                                      
                              if (SelectedRole == Roles[1])
                               {
                                   User.Role = true;
                               }
                               context.Users.Add(User);
                               context.SaveChanges();
                               ApplicantMainWindow amw = new ApplicantMainWindow();
                               amw.DataContext = new AMWvm(User);
                               amw.Show();
                               foreach (Window item in App.Current.Windows)
                               {
                                   if (item.GetType() == typeof(RegWindow))
                                   {
                                       item.Close();
                                   }
                               }
                           }
                           else
                           {
                               WarningText = "Пользователь с таким email уже существует";
                               IsWarningVisible = true;
                           }
                       }                     
                   }));
            }
        }
        //Открытие окна входа
        private RelayCommand _openLogin;
        public RelayCommand OpenLogin
        {
            get
            {
                return _openLogin ??
                   (_openLogin = new RelayCommand(open =>
                   {
                       AuthWindow aw = new AuthWindow();
                       aw.Show();
                       foreach (Window item in App.Current.Windows)
                       {
                           if (item.GetType() == typeof(RegWindow))
                           {
                               item.Close();
                           }
                       }
                   }));
            }
        }       
    }
}
