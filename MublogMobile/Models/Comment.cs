using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MublogMobile.Models
{
    public class Comment : Message
    {

        public const string _GET_COMMENTS_URI = "api/v1/posts/{0}/comments";

        public Comment(int id, string text, User user, DateTime dateCreated) : base(id, text, user, dateCreated) { }

  
        public static async Task<List<Comment>> LoadAll(int postId)
        {        
            var jArray = await GetJArrayFromUriAsync(string.Format(_GET_COMMENTS_URI, postId));
            var comments = new List<Comment>();

            foreach (var jComment in jArray) 
            {
                var parsed = await ParseJMessage(jComment);
                comments.Add(new Comment(parsed.Item1, parsed.Item2, parsed.Item3, parsed.Item4));
            }

            return comments;
        }

    }
}
