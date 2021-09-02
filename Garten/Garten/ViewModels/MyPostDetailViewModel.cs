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


        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { SetProperty(ref _Description, value); }
        }


        private string _Category;
        public string Category
        {
            get { return _Category; }
            set { SetProperty(ref _Category, value); }
        }


        private string _Time;
        public string Time
        {
            get { return _Time; }
            set { SetProperty(ref _Time, value); }
        }


        private List<string> _Images;
        public List<string> Images
        {
            get { return _Images; }
            set { SetProperty(ref _Images, value); }
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
            Images = new List<string>();
            Images = PostDetail.Images;
            Description = PostDetail.Description;

            Titel = PostDetail.Titel;
            Price = PostDetail.Price.ToString();
        }
    }
}
