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

        /*private bool _role;
        public bool Role
        {
            get { return _role; }
            set
            {
                _role = value;
                ChangeRole();
                OnPropertyChanged("Role");
               
            }
        }*/

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
            set {_roles = value;}
        }

        private string _role;
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public RegVM()
        {
            _isWarningVisible = false;

            STSContext context = new STSContext();
            _user = new User();

            _user.Email = string.Empty;
            _user.pw = string.Empty;

            //_role = false;

            Roles = new ObservableCollection<string>()
            {
                "Соискатель",
                "Работодатель"
            };

            _role = "Соискатель";

            _warningText= "фывфыв";
        }

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
                               User user1 = new User(); 
                               ApplicantMainWindow amw = new ApplicantMainWindow(user1);

                              // _user.Role = true; //СДЕЛАЙ ПРИВЯЗКУ РОЛИ
                              if (Role == "Работодатель")
                               {
                                   _user.Role = true;
                               }

                               context.Database.ExecuteSqlRaw($" INSERT INTO \"STS\".\"STS\".users  (Firstname, Lastname, Patronymic, Email, Role, pw) VALUES ('{_user.Firstname}', '{_user.Lastname}', '{_user.Patronymic}', '{_user.Email}', '{_user.Role}', '{_user.pw}');");

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

        private RelayCommand _selectedRoleCommand;
        public RelayCommand SelectedRoleCommand
        {
            get
            {
                return _selectedRoleCommand ??
                    (_selectedRoleCommand = new RelayCommand(role =>
                    {
                        User.Email = "lal";
                        //OnPropertyChanged(_user.Email);
                    }));
            }
        }

        public void ChangeRole()
        {
           // _user.Role = _role == false ? _role = true : _role = false;
             
            //_user.Email = "lol";
            //SelectedRoleCommand.Execute(null);
        }

    }
}
