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

        public Post Post { get; }

        public string Text => this.Post.Text;
        public string ImageSource => this.Post.User.ImageUrl;
        public string Alias => this.Post.User.DisplayName;
        public string Username => "@" + this.Post.User.Alias;
        public string Date => this.Post.DateCreated.ToString("dd/MM/yy");
        public int Comments => this.Post.CommentAmount;
        public string LikeIconSource => this.Post.IsLiked ? _LIKE_FILLED_SOURCE : _LIKE_EMPTY_SOURCE;
        public int Likes => this.Post.Likes;

        public ICommand LikeTapCommand { get; } 

        public PostViewModel(Post post)
        {
            this.Post = post;
            this.LikeTapCommand = new Command(OnLikeTapped);
        }

        public void OnLikeTapped()
        {
            this.Post.IsLiked = !this.Post.IsLiked;
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