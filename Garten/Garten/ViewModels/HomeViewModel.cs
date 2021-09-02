using Garten.Interface;
using Garten.Model;
using Garten.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Garten.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private string _PostsNumber;
        public string PostsNumber
        {
            get { return _PostsNumber; }
            set { SetProperty(ref _PostsNumber, value); }
        }


        private Post _MyPostDetail;
        public Post MyPostDetail
        {
            get { return _MyPostDetail; }
            set
            {
                SetProperty(ref _MyPostDetail, value);

                if (MyPostDetail != null)
                {
                    NavigatoToPage("MyPostDetail", "MyPostDetail", MyPostDetail);
                    MyPostDetail = null;

                }
            }
        }


        private Post _Post;
        public Post Post
        {
            get { return _Post; }
            set
            {
                SetProperty(ref _Post, value);

                if (Post != null)
                {
                    NavigatoToPage("MyPostDetail", "MyPostDetail", Post);
                    Post = null;

                }
            }
        }


        private ObservableCollection<Post> _Posts;
        public ObservableCollection<Post> Posts
        {
            get { return _Posts; }
            set { SetProperty(ref _Posts, value); }
        }

        public static DelegateCommand ShowAllMyPosts { get; set; }
        public DelegateCommand ShowAllPosts { get; set; }
        IEventAggregator _ea;
        INavigationService NavigationService;
        public HomeViewModel(INavigationService navigationService )
        {
            NavigationService = navigationService;
            getPostsData();
            
            //Posts = new ObservableCollection<Post>
            //{
            //    new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},
            //     new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},
            //     new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},
            //     new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},
            //     new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},
            //     new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},
            //     new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},
            //     new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},
            //     new Post {Titel = "Post1 Titel" , Description = "post1 description" , image = "post3.png" , Price = 29},
            //    new Post {Titel = "Post2 Titel" , Description = "post2 description" , image = "post2.jpg" , Price = 29},
            //    new Post {Titel = "Post3 Titel" , Description = "post3 description" , image = "post3.jpg" , Price = 29},

            //};

            PostsNumber = Posts.Count().ToString();
            ShowAllMyPosts = new DelegateCommand(ShowMyPosts);
            ShowAllPosts = new DelegateCommand(ShowPosts);

        }

        private void getPostsData()
        {
            PostService ps = new PostService();
            Posts = new ObservableCollection<Post>();
            Posts = ps.getPosts();

        }

        private void ShowPosts()
        {
            NavigatoToPage("posts", "Posts", Posts);

            //var navigationParams = new NavigationParameters
            //{
            //     { "posts", Posts }
            //};

            //await NavigationService.NavigateAsync("Posts", navigationParams);
        }

        private async void NavigatoToPage<T>(string titel, string page, T data)
        {
            var navigationParams = new NavigationParameters
            {
                 { titel, data }
            };

            await NavigationService.NavigateAsync(page, navigationParams);
        }

        private async void ShowMyPosts()
        {
            var navigationParams = new NavigationParameters
            {
                 { "posts", Posts }
            };

            await NavigationService.NavigateAsync("MyPosts", navigationParams);

        }
    }
}
