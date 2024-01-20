using System;
using Scorpion.Services;
using Scorpion.Models;
using Scorpion.Views.AboutViews;
using Scorpion.Views.OptionViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Scorpion.Views.ProductViews;

namespace Scorpion.Views.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        private static readonly ContentPage OptionPage = new OptionPage();
        private static readonly ContentPage ProductListPage = new ProductListPage();
        private static readonly ContentPage AboutPage = new AboutPage();

        public WelcomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await SqliteAccess.CheckAccessToStorage();
        }


        private void Options_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(OptionPage);
        }

        private void About_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(AboutPage);
        }

        private void Products_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(ProductListPage);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryFinder.Text) || string.IsNullOrWhiteSpace(EntryFinder.Text))
            {
                ItemList.ItemsSource = null;
                return;
            }
            ItemList.ItemsSource = ArticleService.GetArticleListByWord(EntryFinder.Text);
        }

        private void ItemList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ItemList.SelectedItem as Article;
            if (item == null)
                return;
            if (ItemList.SelectedItem is Article article)
                Navigation.PushAsync(new ArticleViews.ArticlePage(article));
            ItemList.SelectedItem = null;
        }

    }
}