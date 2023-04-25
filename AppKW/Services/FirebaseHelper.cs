using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using AppKW.Models;

namespace AppKW.Services
{
    internal class FirebaseHelper
    {
        private readonly string ChildName = "Posts";

        readonly FirebaseClient firebase = new FirebaseClient("https://xamarinapptest-6e415-default-rtdb.firebaseio.com/");

        public async Task<List<Post>> GetAllPosts()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Post>()).Select(item => new Post
                {
                    Title = item.Object.Title
                }).ToList();
        }

        public async Task AddPost(string title)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Post() { PostId = Guid.NewGuid(), Title = title });
        }

        public async Task<Post> GetPost(Guid postId)
        {
            var allPosts = await GetAllPosts();
            await firebase
                .Child(ChildName)
                .OnceAsync<Post>();
            return allPosts.FirstOrDefault(a => a.PostId == postId);
        }

        public async Task<Post> GetPost(string title)
        {
            var allPosts = await GetAllPosts();
            await firebase
                .Child(ChildName)
                .OnceAsync<Post>();
            return allPosts.FirstOrDefault(a => a.Title == title);
        }
    }
}
