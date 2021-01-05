using MublogMobile.Models;
using MublogMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MublogMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentView : ContentView
    {
        private readonly CommentViewModel _model;

        public CommentView(Comment comment)
        {
            this._model = new CommentViewModel(comment);
            this.BindingContext = this._model;
            this.InitializeComponent();
        }

        //todo: check if this belongs here or in the viewModel
        private void _OnUserTapped(object _, EventArgs __) => this.Navigation.PushAsync(new ProfilePage(this._model.Comment.User));
    }
}