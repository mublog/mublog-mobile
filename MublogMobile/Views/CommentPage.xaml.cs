using MublogMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MublogMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentPage : ContentPage
    {
        public CommentPage(Post post)
        {
            this.InitializeComponent();
            this.postView.SetPost(post);

            var stack = this.slComments.Children;
            var task = post.GetCommentsAsync();
            task.Wait();
            var comments = task.Result;

            foreach (var comment in comments) //todo: shouldnt be added in code
                stack.Add(new CommentView(comment));
        }
    }
}