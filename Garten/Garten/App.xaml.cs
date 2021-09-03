using Garten.ViewModels;
using Garten.Views;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Garten
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Preferences.Get("myFirebaseRefreshToken", "")))
            {
                await NavigationService.NavigateAsync("NavigationPage/MainTabbedPage");
            }
            else
            {
                await NavigationService.NavigateAsync("NavigationPage/Login");
            }


            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<Login, LoginViewModel>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Home, HomeViewModel>();
           
            containerRegistry.RegisterForNavigation<MyPosts, MyPostsViewModel>();
            containerRegistry.RegisterForNavigation<Posts, PostsViewModel>();
            containerRegistry.RegisterForNavigation<MyPostDetail, MyPostDetailViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            
            containerRegistry.RegisterForNavigation<Search, SearchViewModel>();
            containerRegistry.RegisterForNavigation<AddPost, AddPostViewModel>();
            containerRegistry.RegisterForNavigation<Messages, MessagesViewModel>();
            containerRegistry.RegisterForNavigation<Account, AccountViewModel>();
            containerRegistry.RegisterForNavigation<Messages, MessagesViewModel>();
            containerRegistry.RegisterForNavigation<Account, AccountViewModel>();
            containerRegistry.RegisterForNavigation<ImagePopup, ImagePopupViewModel>();
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterForNavigation<Register, RegisterViewModel>();
        }
    }
}
