using Xamarin.Forms;

namespace Scorpion.Messages
{
    public class OpenPageMessage
    {
        public OpenPageMessage(ContentPage page)
        {
            Page = page;
        }

        public ContentPage Page { get; set; }
    }
}