using MublogMobile.Models;

namespace MublogMobile.ViewModels
{
    class CommentViewModel : MessageViewModel
    {
        public Comment Comment { get; }

        public CommentViewModel(Comment comment) :base(comment)
        {
            this.Comment = comment;
        }
    }
}
