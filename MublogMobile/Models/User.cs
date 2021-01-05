using MublogMobile.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MublogMobile.Models
{
    public class User
    {

        public string Alias { get; }
        public string DisplayName { get; }
        public string ImageUrl { get; } //todo: cache profile image


        private const string _EMPTY_URL = "00000000-0000-0000-0000-000000000000";

        private static readonly string[] _placeHolderUrls = new[]
        {
            "https://i.imgur.com/TxKhwr9.png",
            "https://i.imgur.com/Rc6yk8q.png",
            "https://i.imgur.com/nFH8Lif.png",
            "https://i.imgur.com/rjLAirw.png",
            "https://i.imgur.com/fCFwZlQ.png",
            "https://i.imgur.com/dklRLm2.png",
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


        public static User GetOrCreateUser(JToken jUser)
        {
            var existingUsers = MainLogic.Instance.AllUsers;
            var alias = (string)jUser["alias"];

            var user = existingUsers.Where(u => u.Alias == alias).FirstOrDefault();
            if (user == null) // no user found
            {
                var displayName = (string)jUser["displayName"];
                var profileUrl = (string)jUser["profileImageUrl"];            

                user = profileUrl == _EMPTY_URL
                    ? new User(alias, displayName)
                    : new User(alias, displayName, profileUrl);

                existingUsers.Add(user);
            }

            return user;
        }

        public string GetJsonLogin(string password)
            => $"{{\"username\": \"{this.Alias}\",\"password\": \"{password}\"}}";

    }
}
