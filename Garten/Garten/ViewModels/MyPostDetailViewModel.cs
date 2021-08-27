using Garten.Interface;
using Garten.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Garten.ViewModels
{
    public class MyPostDetailViewModel : BindableBase, INavigationAware
    {
        #region Post props


        private string _Image;
        public string Image
        {
            get { return _Image; }
            set { SetProperty(ref _Image, value); }
        }

        private string _Titel;
        public string Titel
        {
            get { return _Titel; }
            set { SetProperty(ref _Titel, value); }
        }

        private string _Price;
        public string Price
        {
            get { return _Price; }
            set { SetProperty(ref _Price, value); }
        }
        #endregion



        private Post  _PostDetail;
        public Post PostDetail
        {
            get { return _PostDetail; }
            set { SetProperty(ref _PostDetail, value); }
        }

        public DelegateCommand Back { get; set; }
        INavigationService _nav;
        public MyPostDetailViewModel(INavigationService navigationService
            )
            
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
            PostDetail = parameters.GetValue<Post>("MyPostDetail");
            FillPostData();
            //PostDetail = parameters.GetValue<Post>("MyPost");
        }

        private void FillPostData()
        {
            Image = PostDetail.image;
            Titel = PostDetail.Titel;
            Price = PostDetail.Price.ToString();
        }
    }
}
