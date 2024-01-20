using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Scorpion.Views.AboutViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                System.Uri uri = new("https://github.com/iceheda");
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch
            {

            }
        }
    }
}