using Firebase.Auth;
using Garten.Model;
using Garten.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace Garten.ViewModels
{
    public class RegisterViewModel : BindableBase
    {
        INavigationService _nav;
        public string WebApiKey = "AIzaSyD_rzOMR_cTLm9JYCo1WylNJqXOc56rTvI";

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


        private string _FistName;
        public string FirstName
        {
            get { return _FistName; }
            set { SetProperty(ref _FistName, value); }
        }


        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { SetProperty(ref _LastName, value); }
        }


        private DelegateCommand _Login;
        public DelegateCommand Login =>
        _Login ?? (_Login = new DelegateCommand(CommandMthode));

        async void CommandMthode()
        {
            await _nav.NavigateAsync("Login");
        }


        private DelegateCommand _Register;
        public DelegateCommand Register =>
        _Register ?? (_Register = new DelegateCommand(RegisterM));

       async  void RegisterM()
        {
            Emails = service.getUsers();
            if(Emails.Count() <= 0)
            {
                CreateUserFirebaseAuth();
                RegisterUserRealtimeDatabase();
            }
            
            foreach (var item in Emails)
            {
                if(Email == item.Email)
                {
                   await App.Current.MainPage.DisplayAlert("Alert", "Email already exist", "ok");
                    break;
                }
                else
                {
                    CreateUserFirebaseAuth();
                    RegisterUserRealtimeDatabase();
                    break;
                    
                }
            }
            



        }

        private async void CreateUserFirebaseAuth()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));

            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);

            var token = auth.FirebaseToken;
            var SerializedContent = JsonConvert.SerializeObject(token);
            Preferences.Set("myFirebaseRefreshToken", SerializedContent);

            NavigatoToPage("login", "MainTabbedPage", Email);
        }

        private async void RegisterUserRealtimeDatabase()
        {
            MyUser myUser = new MyUser() { Email = Email, FirstName = FirstName, LastName = LastName, Password = Password };
            await service.AddUser(myUser);
        }

        private async void NavigatoToPage<T>(string titel, string page, T data)
        {
            var navigationParams = new NavigationParameters
            {
                 { titel, data }
            };

            await _nav.NavigateAsync(page, navigationParams);
        }
        //ctor
        PostService service;

        private ObservableCollection<MyUser> _Emails;
        public ObservableCollection<MyUser> Emails
        {
            get { return _Emails; }
            set { SetProperty(ref _Emails, value); }
        }
        public RegisterViewModel(INavigationService navigationService)
        {
            _nav = navigationService;
            service = new PostService();
            Emails = new ObservableCollection<MyUser>();
            
        }
    }
}
