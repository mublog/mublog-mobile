using MublogMobile.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MublogMobile.ViewModels
{
    public class PostViewModel : INotifyPropertyChanged
    {

        private string _LIKE_EMPTY_SOURCE = "likeEmpty.png";
        private const string _LIKE_FILLED_SOURCE = "likeFilled.png";

        private readonly Post _post;

        public string Text => this._post.Text;
        public string ImageSource => this._post.User.ImageUrl;
        public string Alias => this._post.User.DisplayName;
        public string Username => "@" + this._post.User.Alias;
        public int Comments => 0;
        public string LikeIconSource => this._post.IsLiked ? _LIKE_FILLED_SOURCE : _LIKE_EMPTY_SOURCE;
        public int Likes => this._post.Likes;

        public ICommand LikeTapCommand { get; } 

        public PostViewModel(Post post)
        {
            this._post = post;
            this.LikeTapCommand = new Command(OnLikeTapped);
        }

        public void OnLikeTapped()
        {
            this._post.IsLiked = !this._post.IsLiked;
            this.OnPropertyChanged(nameof(Likes));
            this.OnPropertyChanged(nameof(LikeIconSource));
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
          => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

    }
}