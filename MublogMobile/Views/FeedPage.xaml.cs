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

        //todo: do this with bindings instead
        public void LoadMessages()
        {
            var logic = this._logic;

            while (!logic.IsInitialized)
                Task.Delay(50);

            var user = logic.CurrentUser;
            var stack = this.SlMessages.Children;
            var posts = logic.GetAllPosts();

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