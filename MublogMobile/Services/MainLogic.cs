using MublogMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

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
        public HttpClient Client { get; } = new HttpClient();
        public static readonly Uri API_URI = new Uri("https://mublog.xyz/");

        public User CurrentUser { get; private set; }
        private List<Post> _posts;
        public List<User> AllUsers { get; } = new List<User>();

        private MainLogic()
        {
            this.Client.BaseAddress = API_URI;
        }

        public async void Init()
        {
            this._posts = (await Post.LoadAll());
            this.CurrentUser = this.AllUsers.FirstOrDefault();
            //this.LoginAsync();        

            this.IsInitialized = true;
        }

        public async void LoginAsync()
        {           
            var jsonLogin = this.CurrentUser.GetJsonLogin("password");
            var content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var response = await this.Client.PostAsync("/api/v1/accounts/login", content);
            //var result = await response.Content.ReadAsStringAsync();
            //todo: check if result is okay
        }

        public List<Post> GetAllPosts() => this._posts;
        public List<Post> GetPostsFrom(User user) => this._posts.Where(p => p.User.Alias == user.Alias).ToList();

    }
}