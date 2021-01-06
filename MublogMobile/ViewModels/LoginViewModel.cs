using MublogMobile.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MublogMobile.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _password;
        private string _loginErrorText;

        public ICommand LoginCommand { get; }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                this.OnPropertyChanged();
            }
        }

        //todo: shouldnt be plain text
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                this.OnPropertyChanged();
            }
        }

        public string LoginErrorText
        {
            get => _loginErrorText;
            set
            {
                _loginErrorText = value;
                this.OnPropertyChanged();
            }
        }


        public event EventHandler LoggedIn;

        public LoginViewModel()
        {
            this.LoginCommand = new Command(Login);
        }

        public async void Login()
        {
            var name = this.UserName;
            var password = this.Password;

            if (name == string.Empty || name == null || password == string.Empty || password == null)
            {
                this.LoginErrorText = "Bitte fülle alle Felder aus!";
                return;
            }

            if (!await MainLogic.Instance.TryLoginAsync(name.ToLower(), password))
            {
                this.LoginErrorText = "Benutzername und Passwort stimmen nicht überein!";
                return;
            }

            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.LoginErrorText = string.Empty;
            this.LoggedIn?.Invoke(this, null);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
          => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}