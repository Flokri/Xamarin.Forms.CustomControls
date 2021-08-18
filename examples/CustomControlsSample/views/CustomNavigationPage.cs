using Xamarin.Forms;

namespace CustomControlsSample.views
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page page) : base(page)
        {
            this.BarTextColor = Color.White;
            this.BarBackgroundColor = Color.FromHex("#02457A");
        }
    }
}
