using MublogMobile.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MublogMobile.Models
{
    public class Post
    {
        private bool _isLiked;

        public string Text { get; }
        public User User { get; }
        public DateTime DateCreated { get; }
        public int Likes { get; private set; }

        private const string _GET_POSTS_URI = "/api/v1/posts?Page=1&Size=30";

        public bool IsLiked
        {
            get => this._isLiked;
            set
            {
                if (this.IsLiked == value)
                    return;

                this._isLiked = value;

                if (value == true)
                    ++this.Likes;
                else
                    --this.Likes;
            }
        }

        public Post(string text, User user, DateTime dateCreated, int likes)
        {
            this.Text = text;
            this.User = user;
            this.DateCreated = dateCreated;
            this.Likes = likes;
        }

        //todo: handle loading errors properly
        public static async Task<List<Post>> LoadAll()
        {
            var logic = MainLogic.Instance;
            var client = logic.Client;

            //var response = await
            var task = client.GetAsync(_GET_POSTS_URI);
            task.Wait(); //todo: didnt load async for some reason

            var response = task.Result;
            var result = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(result);
            var jData = jObject["data"];
            var jDataString = jData.ToString();
            var jArray = JArray.Parse(jDataString);
            var posts = new List<Post>();

            foreach (var jPost in jArray)
            {
                var jUser = jPost["user"];
                var user = logic.GetOrCreateUser((string)jUser["alias"], (string)jUser["displayName"]);
                var time = (int)jPost["datePosted"];
                var time2 = Utils.UnixTimeStampToDateTime(time);
                posts.Add(new Post((string)jPost["textContent"], user, time2, (int)jPost["likeAmount"]));
            }

            return posts;
        }

    }
}
