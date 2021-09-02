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
    public class PostService
    {
        FirebaseClient client;
        FirebaseStorage storage;

        public PostService()
        {
            client = new FirebaseClient("https://gardenservice-ec613-default-rtdb.firebaseio.com");
            storage = new FirebaseStorage("gardenservice-ec613.appspot.com");
        }

        public async Task AddPost(Post post)
        {
            await client.Child("Posts").PostAsync(post);

        }

        public async Task<string> UploadImage(Stream stream, string imageNumber, string Titel)
        {
            var storageImage = await storage.Child("Images").Child(Titel).Child(imageNumber).PutAsync(stream);
            string imgUrl = storageImage;
            return imgUrl;
        }

        public ObservableCollection<Post> getPosts()
        {
            var postData = client.Child("Posts").AsObservable<Post>()
                                                .AsObservableCollection();

            return postData;
        }
    }
}
