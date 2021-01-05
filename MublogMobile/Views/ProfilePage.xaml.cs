using MublogMobile.Models;
using MublogMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MublogMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {

        private readonly ProfileViewModel _model;

        public ProfilePage()
        {
            var model = new ProfileViewModel();
            this._model = model;
            this.Initialize(model);
        }

        public ProfilePage(User user)
        {
            var model = new ProfileViewModel(user);
            this._model = model;
            this.Initialize(model);
        }

        private void Initialize(ProfileViewModel model)
        {
            this.BindingContext = model;
            this.InitializeComponent();
            this._model.GetPosts.ForEach(p => this.SlMessages.Children.Add(new PostView(p)));
        }

        private void _OnImageNameTapped(object _, EventArgs __) => this._model.OpenProfilePictureAsync();
    }
}