using MublogMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MublogMobile.Services
{
    public class MainLogic
    {

        public static MainLogic Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainLogic();

                return _instance;
            }
        }

        public bool IsInitialized;
        private static MainLogic _instance;
        public HttpClient _client = new HttpClient();
        public static readonly Uri API_URI = new Uri("https://mublog.xyz/");

        public User CurrentUser { get; private set; }
        private List<Post> _posts;
        public List<User> AllUsers { get; } = new List<User>();

        private MainLogic()
        {
            this._client.BaseAddress = API_URI;
        }

        public async void Init()
        {
            this._posts = (await Post.LoadAll());
            this.CurrentUser = this.AllUsers.FirstOrDefault();
            this.IsInitialized = true;
        }

        public async Task<bool> TryLoginAsync(string name, string password)
        {
            var result = await User.TryLoginAsync(name, password);
            this.CurrentUser = result.Item2;
            return result.Item1;
        }

        public async Task<string> GetClientResultAsync(string requestUri)
        {
            var task = this._client.GetAsync(requestUri);
            task.Wait(); //todo: doesnt load async for some reason
            var response = task.Result;

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostClientResultAsync(string requestUri, StringContent content)
        {
            var task = this._client.PostAsync(requestUri, content);
            task.Wait(); //todo: doesnt load async for some reason
            var response = task.Result;

            return await response.Content.ReadAsStringAsync();
        }

        public List<Post> GetAllPosts() => this._posts;
        public List<Post> GetPostsFrom(User user) => this._posts.Where(p => p.User.UserName == user.UserName).ToList();

    }
}