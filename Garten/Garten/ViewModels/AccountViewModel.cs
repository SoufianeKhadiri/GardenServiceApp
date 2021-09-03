using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
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


        private DelegateCommand _Logout;
        public DelegateCommand Logout =>
        _Logout ?? (_Logout = new DelegateCommand(LogoutM));

        async void LogoutM()
        {
            Preferences.Remove("myFirebaseRefreshToken");
            await _nav.NavigateAsync("Login"); 
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
