using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MublogMobile.Models
{
    public class Post : Message
    {
        private bool _isLiked;
        private List<Comment> _comments;

        public int Likes { get; private set; }

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

        public int CommentAmount { get; }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            if (this._comments == null)
                this._comments = await Comment.LoadAll(this.Id);

            return this._comments;
        }

        private const string _GET_POSTS_URI = "/api/v1/posts?Page=1&Size=30";

        private Post(int id, string text, User user, DateTime dateCreated, int likes, int commentAmount) : base(id, text, user, dateCreated)
        {
            this.Likes = likes;
            this.CommentAmount = commentAmount;
        }

        public static async Task<List<Post>> LoadAll()
        {

            var jArray = await GetJArrayFromUriAsync(_GET_POSTS_URI);
            var posts = new List<Post>();

            foreach (var jPost in jArray)
            {
                var parsed = ParseJMessage(jPost);
                posts.Add(new Post(parsed.Item1, parsed.Item2, parsed.Item3, parsed.Item4, (int)jPost["likeAmount"], (int)jPost["commentsAmount"]));
            }

            return posts;
        }

    }
}
