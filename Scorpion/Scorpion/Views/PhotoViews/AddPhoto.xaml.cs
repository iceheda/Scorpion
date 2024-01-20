using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Scorpion.Models;
using Scorpion.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.PhotoViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPhoto : ContentPage
    {
        private int _id;
        private string _path;

        public AddPhoto(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            //var pickFile = await CrossFilePicker.Current.PickFile();
            var pickFile = await FilePicker.PickAsync();

            if (pickFile != null)
            {
                path.Text = "Путь:" + pickFile.FullPath;
                _path = pickFile.FullPath;
            }

            if (File.Exists(_path)) Image1.Source = ImageSource.FromFile(_path);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            var _image = File.ReadAllBytes(_path);

            PhotoService.SavePhoto(new Photo { ArticleId = _id, PhotoBlob = _image }).Wait();
            ToastService.ToastShow("Изображение было успешно загружено");
            Navigation.PopAsync();
            //var photos = PhotoService.GetImagesByArticleId(1);
            //Stream ms = new MemoryStream(photos[2].PhotoBlob);
            //Image1.Source = ImageSource.FromStream(() => ms);
        }

        private async void Cancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}