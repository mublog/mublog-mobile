using MublogMobile.Services;
using Xamarin.Forms;

namespace MublogMobile
{
    public partial class App : Application
    {

        public App()
        {
            MainLogic.Instance.Init();
            this.InitializeComponent();
            this.MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
