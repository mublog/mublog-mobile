using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MublogMobile.Models
{
    public class User
    {

        public string Alias { get; }
        public string DisplayName { get; }
        public string ImageUrl { get; } //todo: cache profile image

        private static readonly string[] _placeHolderUrls = new[]
        { "https://i.imgur.com/kDyxE62.png",
            "https://i.imgur.com/p0mTy2z.png",
            "https://i.imgur.com/iL9cLcG.png"
        };

        private static int _placeHolderIndex = 0;


        public User(string alias, string displayName)
        {
            this.Alias = alias;
            this.DisplayName = displayName;
            this.ImageUrl = _placeHolderUrls[_placeHolderIndex];
            _placeHolderIndex = (_placeHolderIndex + 1) % _placeHolderUrls.Length;
        }

        public User(string alias, string displayName, string imageUrl)
        {
            this.Alias = alias;
            this.DisplayName = displayName;
            this.ImageUrl = imageUrl;
        }

        public static User FromJson(JToken jToken)
            => new User((string)jToken["alias"], (string)jToken["displayName"], (string)jToken["profileImageUrl"]);


        public string GetJsonLogin(string password)
            => $"{{\"username\": \"{this.Alias}\",\"password\": \"{password}\"}}";

    }
}
