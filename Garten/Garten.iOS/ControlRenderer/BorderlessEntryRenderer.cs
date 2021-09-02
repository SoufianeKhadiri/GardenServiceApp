using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Garten.CustomUi;
using Garten.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace Garten.iOS
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // remove the border form the entry
            if (Control != null)
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}