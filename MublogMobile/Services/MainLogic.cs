using MublogMobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        public HttpClient Client { get; } = new HttpClient();



        public User CurrentUser { get; private set; }
        private List<Post> _posts;
        private List<User> _allUsers = new List<User>();

        private MainLogic()
        {
            this.Client.BaseAddress = new Uri("http://10.0.2.2:5000");
        }

        public async void Init()
        {
            this._posts = (await Post.LoadAll()).OrderBy(p => p.DateCreated).ToList();
            this.CurrentUser = this._allUsers.FirstOrDefault();
            this.LoginAsync();        

            this.IsInitialized = true;
        }

        public async void LoginAsync()
        {
            var jsonLogin = this.CurrentUser.GetJsonLogin("password");
            var content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var response = await this.Client.PostAsync("/api/v1/accounts/login", content);
            var result = await response.Content.ReadAsStringAsync();
            //todo: check if result is okay
        }

        public User GetOrCreateUser(string name, string displayName)
        {
            var user = this._allUsers.Where(u => u.Alias == name).FirstOrDefault();
            if (user == null) // no user found
            {
                user = new User(name, displayName);
                this._allUsers.Add(user);
            }

            return user;
        }

        public List<Post> GetAllPosts() => this._posts;
        public List<Post> GetPostsFrom(User user) => this._posts.Where(p => p.User.Alias == user.Alias).ToList();

    }
}