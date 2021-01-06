using Xamarin.Forms;

namespace MublogMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            this.InitializeComponent();
            this.Logout();
        }

        public void Login()
        {
            this.loginItem.IsVisible = false;
            this.logoutItem.IsVisible = true;
            this.profileItem.IsVisible = true;
            this.CurrentItem = this.profileItem;
        }

        public void Logout()
        {
            this.loginItem.IsVisible = true;
            this.logoutItem.IsVisible = false;
            this.profileItem.IsVisible = false;
            this.CurrentItem = this.loginItem;
        }

    }
}