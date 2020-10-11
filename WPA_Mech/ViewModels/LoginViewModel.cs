using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPA_Mech.Command;

namespace WPA_Mech.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        private bool _isAuthenticated;
        public bool isAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                if (value != _isAuthenticated)
                {
                    _isAuthenticated = value;
                    OnPropertyChanged("isAuthenticated");
                }
            }
        }
        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("UserName");
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

     /*   public ICommand LoginCommand
        {
           // get { return new RelayCommand2(() => Login()); }
        }*/

        public void Login()
        {
            //TODO check username and password vs database here.
            //If using membershipprovider then just call Membership.ValidateUser(UserName, Password)
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password))
                isAuthenticated = true;
        }

        #region INotifyPropertyChanged Methods

        public void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, args);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}