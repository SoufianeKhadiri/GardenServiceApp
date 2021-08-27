using Garten.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Garten.ViewModels
{
    public class PostsViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Post> _Posts;
        public ObservableCollection<Post> Posts
        {
            get { return _Posts; }
            set { SetProperty(ref _Posts, value); }
        }
        public DelegateCommand Back { get; set; }
        INavigationService _nav;
        public PostsViewModel(INavigationService navigationService)
        {
            _nav = navigationService;
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
            Posts = parameters.GetValue<ObservableCollection<Post>>("posts");
        }
    }
}
