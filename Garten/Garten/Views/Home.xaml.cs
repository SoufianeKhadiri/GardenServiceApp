using Xamarin.Forms;

namespace Garten.Views
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
