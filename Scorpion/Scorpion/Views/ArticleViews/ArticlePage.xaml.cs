using System.Linq;
using Scorpion.Models;
using Scorpion.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.ArticleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticlePage : ContentPage
    {
        public ArticlePage(Article item)
        {
            InitializeComponent();

            ScrollViewArticle.BindingContext = item;
            var items = PhotoService.GetImagesByArticleId(item.Id);
            ListViewImages.ItemsSource = items;

            if (items == null || !items.Any())
                Grid2.IsVisible = false;//.IsVisible = false;
        }

        private void ListViewImages_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListViewImages.SelectedItem = null;
        }
    }
}