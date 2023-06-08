using STS.DAL.Entities;
using STS.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace STS.ViewModels
{
    public class AuthVM : BaseViewModel
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

        public AuthVM()
        {
            _isWarningVisible=false;

            STSContext context = new STSContext();
            _user = new User();

            _user.Email = string.Empty;
            _user.pw = string.Empty;

            /*var user = context.Users.FirstOrDefault(x => x.Id == 2);

            User = user;*/
        }

        //Вход
        private RelayCommand _loginCommand; 

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                    (_loginCommand = new RelayCommand(password =>
                    {
                        _user.pw = (password as PasswordBox).Password;


                        STSContext context = new STSContext();

                        var user = context.Users.SingleOrDefault(x => x.Email == _user.Email && x.pw == _user.pw);

                        if (user != null)
                        {
                            ApplicantMainWindow amw = new ApplicantMainWindow();
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
                            //MessageBox.Show("Ты чо ахуел блять");

                            IsWarningVisible = true;
                        }
                        
                    }));
            }
        }
        
    }
}
