using Garten.Model;
using Garten.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garten.ViewModels
{
    public class ImagePopupViewModel : BindableBase
    {
        IEventAggregator _ea;
        public ImagePopupViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }
        private DelegateCommand _TakeFoto;
        public DelegateCommand TakeFoto =>
        _TakeFoto ?? (_TakeFoto = new DelegateCommand(TakeFotoM));

        void TakeFotoM()
        {
            _ea.GetEvent<TakeFoto>().Publish(true);
            PopupNavigation.Instance.PopAsync(true);
        }

        private DelegateCommand _Gallery;
        public DelegateCommand Gallery =>
        _Gallery ?? (_Gallery = new DelegateCommand(GalleryM));

        void GalleryM()
        {
            _ea.GetEvent<TakeFotoGalery>().Publish(true);
            PopupNavigation.Instance.PopAsync(true);
        }

    }
}