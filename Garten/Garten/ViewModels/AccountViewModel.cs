using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Garten.ViewModels
{
    public class AccountViewModel : BindableBase
    {
        INavigationService _nav;
        public AccountViewModel(INavigationService navigation)
        {
            _nav = navigation;
        }

        private DelegateCommand _GoToMyPosts;
        public DelegateCommand GoToMyPosts =>
        _GoToMyPosts ?? (_GoToMyPosts = new DelegateCommand(GoToMyPostsM));

        async void GoToMyPostsM()
        {
            HomeViewModel.ShowAllMyPosts.Execute();
        }
        //private async void NavigatoToPage<T>(string titel, string page, T data)
        //{
        //    var navigationParams = new NavigationParameters
        //    {
        //         { titel, data }
        //    };

        //    await _nav.NavigateAsync(page, navigationParams);
        //}
    }
}
