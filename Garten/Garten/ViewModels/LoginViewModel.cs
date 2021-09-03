
using Firebase.Auth;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace Garten.ViewModels
{
    public class LoginViewModel : BindableBase
    {

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }


        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }


        public string WebApiKey = "AIzaSyD_rzOMR_cTLm9JYCo1WylNJqXOc56rTvI";

        //ctor
        INavigationService _nav;
        public LoginViewModel(INavigationService navigationService)
        {
            _nav = navigationService;
        }

        private DelegateCommand _LoginCommand;
        public DelegateCommand LoginCommand =>
        _LoginCommand ?? (_LoginCommand = new DelegateCommand(LoginMethode));

        async void LoginMethode()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email,Password);
            var  token = auth.GetFreshAuthAsync();
            var SerializedContent = JsonConvert.SerializeObject(token);
            Preferences.Set("myFirebaseRefreshToken", SerializedContent);

            NavigatoToPage("login", "MainTabbedPage", Email);

            //await App.Current.MainPage.DisplayAlert("Alert", gettoken, "ok");

        }


        private DelegateCommand _Register;
        public DelegateCommand Register =>
        _Register ?? (_Register = new DelegateCommand(RegisterM));  

        async void RegisterM()
        {
            await _nav.NavigateAsync("Register");
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
