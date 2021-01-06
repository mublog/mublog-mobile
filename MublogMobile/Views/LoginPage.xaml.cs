using System;
using MublogMobile.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MublogMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var model = new LoginViewModel();
            model.LoggedIn += OnLogin;
            this.BindingContext = model;
            this.InitializeComponent();
        }

        private void OnLogin(object sender, EventArgs e) => ((AppShell)Shell.Current).Login();

        private async void LinkTapped(object sender, EventArgs e) => await Browser.OpenAsync("https://mublog.xyz");
    }
}