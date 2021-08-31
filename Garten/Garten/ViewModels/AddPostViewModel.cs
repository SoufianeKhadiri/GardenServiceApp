using Garten.Model;
using Garten.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
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
        //ctor
        public AddPostViewModel()
        {
            
            Images = new ObservableCollection<MyImage>();
            ImgSource = "addImage.svg";
            DeleteFotoVisibility = false;


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
        public  ObservableCollection<MyImage> Images
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
            ImgSource = "addImage.svg";
            DeleteFotoVisibility = false;
        }



        private DelegateCommand _TakeFoto;
        public DelegateCommand TakeFoto =>
        _TakeFoto ?? (_TakeFoto = new DelegateCommand(TakeFotoM));

          void TakeFotoM()
        {
             PopupNavigation.Instance.PushAsync(new ImagePopup());
        }


        async void TakeFotoFromCamera()
        {
            try
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,

                    PhotoSize = PhotoSize.Small,
                    Directory = "Xamarin",
                    SaveToAlbum = true
                });
                if (photo != null)
                {

                    var src = ImageSource.FromStream(() => { return photo.GetStream(); });
                    var path = photo.AlbumPath;
                    MyImage img = new MyImage { Source = src, FilePath = path };
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
