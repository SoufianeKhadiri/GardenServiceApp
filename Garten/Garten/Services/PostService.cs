using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Garten.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Garten.Services
{
    public   class PostService
    {
        FirebaseClient client;
        FirebaseStorage storage;

        public  PostService()
        {
            client = new FirebaseClient("https://gardenservice-ec613-default-rtdb.firebaseio.com");
            storage = new FirebaseStorage("gardenservice-ec613.appspot.com");
        }

        public async  Task AddPost(Post post)
        {
            await client.Child("Posts").PostAsync(post);

        }

        public async Task AddUser(MyUser user)
        {
            await client.Child("Users").PostAsync(user);

        }

        public async Task<string> UploadImage(Stream stream, string imageNumber, string Titel , string database)
        {
            var storageImage = await storage.Child(database).Child(Titel).Child(imageNumber).PutAsync(stream);
            string imgUrl = storageImage;
            return imgUrl;
        }

        public  ObservableCollection<Post> getPosts()
        {
            var postData = client.Child("Posts").AsObservable<Post>()
                                                .AsObservableCollection();
           
            return postData;
           
        }
        public ObservableCollection<MyUser> getUsers()
        {
            var UserData = client.Child("Users").AsObservable<MyUser>()
                                                .AsObservableCollection();

            return UserData;
        }
    }
}
