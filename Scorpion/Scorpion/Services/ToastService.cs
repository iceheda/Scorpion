using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace Scorpion.Services
{
    public class ToastService
    {
        public static void ToastShow(string message)
        {
            CrossToastPopUp.Current.ShowCustomToast(message, "#FF848484", "#FFFFFFFF", ToastLength.Long);
        }
    }
}