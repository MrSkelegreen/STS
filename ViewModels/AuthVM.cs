using STS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AuthVM()
        {
            STSContext context = new STSContext();
            _user = new User();

            var user = context.Users.FirstOrDefault(x => x.Id == 2);

            User = user;
        }
    }
}
