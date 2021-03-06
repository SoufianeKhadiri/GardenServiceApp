using Garten.Model;
using Garten.Services;
using Garten.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Garten.ViewModels
{
    public class AddPostViewModel : BindableBase
    {
        #region Post Props

        private string _Titel;
        public string Titel
        {
            get { return _Titel; }
            set { SetProperty(ref _Titel, value); }
        }

        private string _Category;
        public string Category
        {
            get { return _Category; }
            set { SetProperty(ref _Category, value); }
        }

        private string[] _Categories;
        public string[] Categories
        {
            get { return _Categories; }
            set { SetProperty(ref _Categories, value); }
        }


        private string[] _ImagesPath;
        public string[] ImagesPath
        {
            get { return _ImagesPath; }
            set { SetProperty(ref _ImagesPath, value); }
        }


        private int _Price;
        public int Price
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

        #endregion



        PostService postService;
        //ctor
        INavigationService _nav;
        public AddPostViewModel(IEventAggregator ea, INavigationService navigationService)
        {
            _nav = navigationService;
            ea.GetEvent<TakeFoto>().Subscribe(TakeFotoFromCamera);
            ea.GetEvent<TakeFotoGalery>().Subscribe(TakeFotoFromGalery);
            Images = new ObservableCollection<MyImage>();
            ImgSource = "addImage.svg";
            DeleteFotoVisibility = false;
            ImagesStream = new List<Stream>();
            postService = new PostService();
            InitPicker();

        }

        private void InitPicker()
        {
            Categories = new string[] { "Category1", "Category2", "Category3", "Category4", "Category5", "Category6" };
        }

        private DelegateCommand _AddPost;
        public DelegateCommand AddPost =>
        _AddPost ?? (_AddPost = new DelegateCommand(AddPostM));

        async void AddPostM()
        {
            List<string> imagesUrl = new List<string>();
            int imageNumber = 0;
            foreach (var item in ImagesStream)
            {
                imagesUrl.Add(await postService.UploadImage(item,  imageNumber.ToString(), Titel , "Posts"));
                imageNumber++;
            }

           //string imgUrl =  await postService.UploadImage(imgStream, Titel);
            //User usr = new User() { Name = "name" , Adress = "adress" , Email = "email"
            //, Password = "password", Phone = "92863827532" };
            Post p = new Post() {Images = imagesUrl, Category = Category, Price = Price, Titel = Titel, Description =  Description, Time = getCurrentTime() };
            await postService.AddPost(p);

        }

        private string getCurrentTime()
        {
            DateTime localDate = DateTime.Now;
            return localDate.ToString();

        }

        private bool _DeleteFotoVisibility;
        public bool DeleteFotoVisibility
        {
            get { return _DeleteFotoVisibility; }
            set { SetProperty(ref _DeleteFotoVisibility, value); }
        }


        private MyImage _Image;
        public MyImage Image
        {
            get { return _Image; }
            set
            {
                SetProperty(ref _Image, value);
                if (Image != null)
                {
                    ImgSource = Image.Source;
                    imgStream = Image.ImageStream;
                    DeleteFotoVisibility = true;
                }
            }
        }

        private ImageSource _ImgSource;
        public ImageSource ImgSource
        {
            get { return _ImgSource; }
            set { SetProperty(ref _ImgSource, value); }
        }


       

        private ObservableCollection<MyImage> _Images;
        public ObservableCollection<MyImage> Images
        {
            get { return _Images; }
            set { SetProperty(ref _Images, value); }
        }


        private DelegateCommand _DeleteFoto;
        public DelegateCommand DeleteFoto =>
        _DeleteFoto ?? (_DeleteFoto = new DelegateCommand(DeleteFotoM));

        void DeleteFotoM()
        {
            Images.Remove(Image);
            ImagesStream.Remove(Image.ImageStream);
            ImgSource = "addImage.svg";
            DeleteFotoVisibility = false;
        }



        private DelegateCommand _TakeFoto;
        public DelegateCommand TakeFoto =>
        _TakeFoto ?? (_TakeFoto = new DelegateCommand(TakeFotoM));

        async void TakeFotoM()
        {
            //await PopupNavigation.Instance.PushAsync(new ImagePopup());
            await _nav.NavigateAsync("ImagePopup");
            

        }


        private Stream _img;
        public Stream imgStream
        {
            get { return _img; }
            set { SetProperty(ref _img, value); }
        }


        private List<Stream> _ImagesStream;
        public List<Stream> ImagesStream
        {
            get { return _ImagesStream; }
            set { SetProperty(ref _ImagesStream, value); }
        }

        public async void TakeFotoFromCamera(bool obj)
        {
            if (obj == true)
            {
                try
                {
                    var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                    {
                        DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,

                        //PhotoSize = PhotoSize.Small,
                        PhotoSize = PhotoSize.Custom,
                        CustomPhotoSize = 10,
                        Directory = "Xamarin",
                        SaveToAlbum = true
                    });
                    if (photo != null)
                    {

                        var src = ImageSource.FromStream(() => { return photo.GetStream(); });
                        //var path = photo.AlbumPath;
                        MyImage img = new MyImage { Source = src, ImageStream = photo.GetStream() };
                   
                        imgStream = img.ImageStream;
                        ImagesStream.Add(imgStream);
                        Images.Add(img);

                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "ok");

                }
            }
        }

        public async void TakeFotoFromGalery(bool obj)
        {
            if (obj == true)
            {
                try
                {
                    var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
                    {
                        //PhotoSize = PhotoSize.Small
                        PhotoSize = PhotoSize.Custom,
                        CustomPhotoSize = 10
                       
                     });
                    if (photo != null)
                    {

                        var src = ImageSource.FromStream(() => { return photo.GetStream(); });
                        MyImage img = new MyImage { Source = src, ImageStream = photo.GetStream() };

                        imgStream = img.ImageStream;
                        ImagesStream.Add(imgStream);
                        Images.Add(img);

                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "ok");

                }
            }
        }
    }
}
