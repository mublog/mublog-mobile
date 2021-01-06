using MublogMobile.Models;

namespace MublogMobile.ViewModels
{
    public abstract class MessageViewModel
    {
        private readonly Message _message;

        public string Text => this._message.Text;
        public string ImageSource => this._message.User.ImageUrl;
        public string DisplayName => this._message.User.DisplayName;
        public string UserName => "@" + this._message.User.UserName;
        public string Date => this._message.DateCreated.ToString("dd/MM/yy");

        public MessageViewModel(Message message)
        {
            this._message = message;
        }


    }
}
