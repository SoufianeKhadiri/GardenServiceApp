using Foundation;
using Garten.Interface;
using Garten.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarStyleManager))]
namespace Garten.iOS
{
    public class StatusBarStyleManager : IStatusBarStyleManager
    {
        public StatusBarStyleManager()
        {

        }

        public void HideStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        public void ShowStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }
    }
}