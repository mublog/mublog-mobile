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


        private PostViewModel _model;

        public PostView()
        {
            this.InitializeComponent();
        }

        public PostView(Post post) : this()
        {           
            this._model = new PostViewModel(post);
            this.BindingContext = this._model;         
        }

        public void SetPost(Post post)
        {
            this._model = new PostViewModel(post);
            this.BindingContext = this._model;
        }

        //todo: check if this belongs here or in the viewModel
        private void _OnUserTapped(object _, EventArgs __) => this.Navigation.PushAsync(new ProfilePage(this._model.Post.User));
        private void _OnCommentsTapped(object _, EventArgs __) => this.Navigation.PushAsync(new CommentPage(this._model.Post));
    }
}