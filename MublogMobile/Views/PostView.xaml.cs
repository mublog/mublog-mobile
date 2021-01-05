using MublogMobile.Models;
using MublogMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MublogMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostView : ContentView
    {

        private readonly Post _post;
        private readonly PostViewModel _model;

        public PostView(Post post)
        {
            var model = new PostViewModel(post);

            this._post = post;
            this._model = model;
            this.BindingContext = model;
            this.InitializeComponent();
        }

        //todo: check if this belongs here or in the viewModel
        private void _OnUserTapped(object _, EventArgs __) => this.Navigation.PushAsync(new ProfilePage(this._post.User));

    }
}