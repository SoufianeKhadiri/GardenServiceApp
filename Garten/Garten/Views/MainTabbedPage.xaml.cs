using Xamarin.Forms;

namespace Garten.Views
{
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
           
        }
    }
}
