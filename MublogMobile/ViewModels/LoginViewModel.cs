using MublogMobile.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MublogMobile.ViewModels
{
    class LoginViewModel
    {
        public ICommand LoginCommand { get; }
        public string UserName { get; set; }
        public string Password { get; set; } //todo: shouldnt be plain text

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
                return;

            if (!await MainLogic.Instance.TryLoginAsync(name.ToLower(), password))
                return;

            this.LoggedIn?.Invoke(this, null);
        }
    }
}