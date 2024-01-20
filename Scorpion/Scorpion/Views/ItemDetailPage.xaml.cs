using Scorpion.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Scorpion.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}