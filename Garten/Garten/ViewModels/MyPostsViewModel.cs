using Garten.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Garten.ViewModels
{
    public class MyPostsViewModel : BindableBase , INavigationAware
    {
        IEventAggregator _ea;

        private ObservableCollection<Post> _MyPosts;
        public ObservableCollection<Post> MyPosts
        {
            get { return _MyPosts; }
            set { SetProperty(ref _MyPosts, value);
            
            }
        }

        public DelegateCommand Back { get; set; }

        private Post _MyPost;
        public Post MyPost
        {
            get { return _MyPost; }
            set
            {
                SetProperty(ref _MyPost, value);

                if (MyPost != null)
                {
                    NavigatoToPage("MyPostDetail", "MyPostDetail", MyPost);
                    MyPost = null;
                }
            }
        }
        INavigationService _nav;
        public MyPostsViewModel(INavigationService navigationService)
        {
            _nav = navigationService;
            MyPosts = new ObservableCollection<Post>();
            Back = new DelegateCommand(BackM);
        }

        private async void BackM()
        {
            await _nav.GoBackAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Count > 0)
            {
                MyPosts = parameters.GetValue<ObservableCollection<Post>>("posts");
            }
        }
        private async void NavigatoToPage<T>(string titel, string page, T data)
        {
            var navigationParams = new NavigationParameters
            {
                 { titel, data }
            };

            await _nav.NavigateAsync(page, navigationParams);
        }
    }
}
