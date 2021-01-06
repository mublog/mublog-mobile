using MublogMobile.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MublogMobile.Models
{
    public class User
    {

        public string UserName { get; }
        public string DisplayName { get; }
        public string ImageUrl { get; } //todo: cache profile image


        private const string _EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
        private static readonly Uri _MEDIA_URI = new Uri(MainLogic.API_URI,"/api/v1/media/");
        private static string _LOGIN_URI = "/api/v1/accounts/login";
        private static string _USER_URI = "/api/v1/users/{0}";

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


        public User(string userName, string displayName)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.ImageUrl = _placeHolderUrls[_placeHolderIndex];
            _placeHolderIndex = (_placeHolderIndex + 1) % _placeHolderUrls.Length;
        }

        public User(string userName, string displayName, string imageUrl)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.ImageUrl = imageUrl;
        }


        //can only be used together with messages, otherwise parsing doesnt work
        public static User GetOrCreateUser(JToken jUser)
        {
            var existingUsers = MainLogic.Instance.AllUsers;
            var userName = (string)jUser["alias"];

            var user = existingUsers.Where(u => u.UserName == userName).FirstOrDefault();
            if (user == null) // no user found
            {
                var displayName = (string)jUser["displayName"];
                var profileUrl = (string)jUser["profileImageUrl"];            

                user = profileUrl == _EMPTY_GUID
                    ? new User(userName, displayName)
                    : new User(userName, displayName, _MEDIA_URI + profileUrl);

                existingUsers.Add(user);
            }

            return user;
        }

        public static async Task<(bool, User)> TryLoginAsync(string name, string password)
        {
            var jsonLogin = _GetJsonLogin(name, password);
            var content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var result = await MainLogic.Instance.PostClientResultAsync(_LOGIN_URI, content);
            var jObject = JObject.Parse(result);
            var isError = (bool)jObject["isError"];

            return isError ? (false, null) : (true, await LoadUserByNameAsync(name));
        }

        //todo: is partly copy paste atm
        private static async Task<User> LoadUserByNameAsync(string name)
        {
            var result = await MainLogic.Instance.GetClientResultAsync(string.Format(_USER_URI, name));
            var existingUsers = MainLogic.Instance.AllUsers;
            var jObject = JObject.Parse(result);
            var jUser = jObject["data"];

            var userName = (string)jUser["username"];

            var user = existingUsers.Where(u => u.UserName == userName).FirstOrDefault();
            if (user == null) // no user found
            {
                var displayName = (string)jUser["displayName"];
                var profileUrl = (string)jUser["profileImageId"];

                user = profileUrl == _EMPTY_GUID
                    ? new User(userName, displayName)
                    : new User(userName, displayName, _MEDIA_URI + profileUrl);

                existingUsers.Add(user);
            }

            return user;
        }

        private static string _GetJsonLogin(string name, string password)
            => $"{{\"username\": \"{name}\",\"password\": \"{password}\"}}";

    }
}
