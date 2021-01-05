using MublogMobile.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MublogMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage
    {

        private readonly MainLogic _logic = MainLogic.Instance;

        public FeedPage()
        {
            this.InitializeComponent();
            this.LoadMessages();
        }

        public void LoadMessages()
        {
            while (!this._logic.IsInitialized)
            {
                Task.Delay(50);
            }

            var user = this._logic.CurrentUser;
            var stack = this.SlMessages.Children;
            var posts = this._logic.GetAllPosts();

            posts.ForEach(p => stack.Add(new PostView(p)));

            var endLabel = new Label
            {
                Text = "End of Feed..",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            stack.Add(endLabel);
        }

    }
}