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

        private int _role;
        public int Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged("Role");
            }
        }

        public RegVM()
        {
            _isWarningVisible = false;

            STSContext context = new STSContext();
            _user = new User();

            _user.Email = string.Empty;
            _user.pw = string.Empty;

            _role = 1;
        }

        private RelayCommand _regCommand;
        public RelayCommand RegCommand
        {
            get 
            {
                return _regCommand ??
                   (_regCommand = new RelayCommand(password =>
                   {
                       _user.pw = (password as PasswordBox).Password;

                       STSContext context = new STSContext();

                       var user = context.Users.SingleOrDefault(x => x.Email == _user.Email && x.pw == _user.pw);

                       if (user == null)
                       {
                           ApplicantMainWindow amw = new ApplicantMainWindow();

                           _user.Role = true; //СДЕЛАЙ ПРИВЯЗКУ РОЛИ

                           context.Database.ExecuteSqlRaw($" INSERT INTO \"STS\".\"STS\".users  (Firstname, Lastname, Patronymic, Email, Role, pw) VALUES ('{_user.Firstname}', '{_user.Lastname}', '{_user.Patronymic}', '{_user.Email}', '{_user.Role}', '{_user.pw}');");

                           amw.Show();
                           foreach (Window item in App.Current.Windows)
                           {
                               if (item.GetType() == typeof(AuthWindow))
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
