using System;
using Scorpion.Models;
using Scorpion.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.PhotoViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeletePhoto : ContentPage
    {
        private readonly int id;
        public DeletePhoto(int _id)
        {
            InitializeComponent();
            id = _id;

            Refresh();
        }

        private void OneTap_OnTapped(object sender, EventArgs e)
        {
            
        }

        private async void DoubleTap_OnTapped(object sender, EventArgs e)
        {
            var isTrue = await DisplayAlert("Внимание!",
                "Вы действительно хотите удалить выбранную фотографию? Это действие нельзя отменить.",
                "Да", "Нет");
            if (isTrue)
            {
                var item = ListViewImages.SelectedItem as Photo;
                PhotoService.DeletePhoto(item);
                Refresh();
            }
        }

        private void Refresh()
        {
            try
            {
                var listImage = PhotoService.GetImagesByArticleId(id);
                ListViewImages.ItemsSource = listImage;
            }
            catch
            {
            }
        }
    }
}