



using Android.App;
using Android.OS;
using Android.Views;
using Garten.Droid;
using Garten.Interface;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarStyleManager))]
namespace Garten.Droid
{
    public class StatusBarStyleManager : IStatusBarStyleManager
    {
        public StatusBarStyleManager()
        {

        }

        WindowManagerFlags _originalFlags;

        public void HideStatusBar()
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }

        public void ShowStatusBar()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
        }
    }
}