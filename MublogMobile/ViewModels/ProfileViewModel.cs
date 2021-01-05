using MublogMobile.Models;
using MublogMobile.Services;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace MublogMobile.ViewModels
{
    class ProfileViewModel
    {

        private readonly User _user;
        private readonly MainLogic _logic = MainLogic.Instance;
        public string ImageSource => this._user.ImageUrl;
        public string Alias => this._user.DisplayName;
        public string Username => "@" + this._user.Alias;

        public List<Post> GetPosts => this._logic.GetPostsFrom(this._user);

        public ProfileViewModel()
        {
            this._user = _logic.CurrentUser;
        }

        public ProfileViewModel(User user)
        {
            this._user = user;
        }

        public async void OpenProfilePictureAsync() => await Browser.OpenAsync(this._user.ImageUrl);

    }
}