using MublogMobile.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        private static readonly Regex _linkRegex = new Regex(@"\[(.+)\]\((.+)\)");


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
            text = _ParseText(text);
            this.Text = text;
            

            this.User = user;
            this.DateCreated = dateCreated;
            this.Likes = likes;
        }


        private static string _ParseText(string text)
        {
            var match = _linkRegex.Match(text);
            while(match.Success)
            {
                var wholeLink = match.Groups[0].Value;
                var linkDescription = match.Groups[1].Value;
                text = text.Replace(wholeLink, linkDescription);
                match = match.NextMatch();
            }

            return text;
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
                var user = User.GetOrCreateUser(jPost["user"]);
                var time = (int)jPost["datePosted"];
                posts.Add(new Post((string)jPost["textContent"], user, Utils.UnixTimeStampToDateTime(time), (int)jPost["likeAmount"]));
            }

            return posts;
        }

    }
}
