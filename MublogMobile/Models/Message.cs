using MublogMobile.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MublogMobile.Models
{
    public abstract class Message
    {
        protected int Id { get; }
        public string Text { get; }
        public User User { get; }
        public DateTime DateCreated { get; }

        private static readonly Regex _linkRegex = new Regex(@"\[(.+)\]\((.+)\)");
        private static readonly Regex _emptyLinesRegex = new Regex(@"[\r\n]$");

        private static string _ParseText(string text)
        {
            //remove links and images
            var match = _linkRegex.Match(text);
            while (match.Success)
            {
                var wholeLink = match.Groups[0].Value;
                var linkDescription = match.Groups[1].Value;
                text = text.Replace(wholeLink, linkDescription);
                match = match.NextMatch();
            }

            //remove empty lines
            text = _emptyLinesRegex.Replace(text, string.Empty);

            return text;
        }

        protected Message(int id, string text, User user, DateTime dateCreated)
        {
            this.Id = id;
            this.Text = _ParseText(text);
            this.User = user;
            this.DateCreated = dateCreated;
        }

        //todo: handle loading errors properly
        protected static async Task<JArray> GetJArrayFromUriAsync(string uri)
        {
            var result = await MainLogic.Instance.GetClientResultAsync(uri);

            var jObject = JObject.Parse(result);
            var jData = jObject["data"];
            var jDataString = jData.ToString();
            return JArray.Parse(jDataString);
        }

        protected static (int, string, User, DateTime) ParseJMessage(JToken jMessage) => (
                (int)jMessage["id"],
                (string)jMessage["textContent"],
                User.GetOrCreateUser(jMessage["user"]),
                Utils.UnixTimeStampToDateTime((int)jMessage["datePosted"])
                );

    }
}
