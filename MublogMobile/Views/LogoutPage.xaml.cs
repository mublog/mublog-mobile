using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MublogMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            this.InitializeComponent();
        }

        private void LogoutClicked(object __, EventArgs _) => ((AppShell)Shell.Current).Logout();
    }
}