using MublogMobile.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MublogMobile.ViewModels
{
    public class PostViewModel : MessageViewModel, INotifyPropertyChanged
    {

        private string _LIKE_EMPTY_SOURCE = "likeEmpty.png";
        private const string _LIKE_FILLED_SOURCE = "likeFilled.png";

        private string _COMMENT_EMPTY_SOURCE = "commentEmpty.png";
        private const string _COMMENT_FILLED_SOURCE = "commentFilled.png";

        public Post Post { get; }

        public int CommentAmount => this.Post.CommentAmount;
        public string CommentIconSource => this.CommentAmount == 0 ? _COMMENT_EMPTY_SOURCE : _COMMENT_FILLED_SOURCE;

        public string LikeIconSource => this.Post.IsLiked ? _LIKE_FILLED_SOURCE : _LIKE_EMPTY_SOURCE;
        public int Likes => this.Post.Likes;

        public ICommand LikeTapCommand { get; } 

        public PostViewModel(Post post) : base(post)
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