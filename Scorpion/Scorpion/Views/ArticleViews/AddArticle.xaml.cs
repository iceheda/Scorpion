

using System;
using Microsoft.Data.Sqlite;
using Scorpion.Models;
using Scorpion.Services;
using Scorpion.Views.OptionViews;
using Scorpion.Views.PhotoViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.ArticleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddArticle : ContentPage
    {
        private int _id;
        private bool _isEdit = true;
        private Article _item;
        private bool _wasClicked; //защита от двойного нажатия по button

        public AddArticle(Article item, int id)
        {
            InitializeComponent();

            _id = id;
            if (item == null)
            {
                _isEdit = false;
            }
            else
            {
                _item = item;
                nameEntry.Text = item.Name;
                FullDescriptionEntry.Text = item.FullDescription;
                ShortDescEntry.Text = item.ShortDescription;
            }

            if (_isEdit == false)
            {
                ButtonAdd.IsVisible = false;
                ButtonDelete.IsVisible = false;
            }
        }

        private async void Cancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nameEntry.Text) & !string.IsNullOrEmpty(FullDescriptionEntry.Text) & !string.IsNullOrEmpty(ShortDescEntry.Text))
            {
                if (_wasClicked == false)
                {
                    _wasClicked = true;

                    if (_isEdit == false)
                    {
                        if (!string.IsNullOrWhiteSpace(nameEntry.Text) &&
                            !string.IsNullOrWhiteSpace(ShortDescEntry.Text) &&
                            !string.IsNullOrWhiteSpace(FullDescriptionEntry.Text))
                        {
                            var item = new Article
                            {
                                Name = nameEntry.Text.ReplaceWhiteSpaces(),
                                FullDescription = FullDescriptionEntry.Text.ReplaceWhiteSpaces(),
                                ShortDescription = ShortDescEntry.Text.ReplaceWhiteSpaces(),
                                SubsectionId = _id
                            };

                            ArticleService.SaveArticle(item);

                            nameEntry.Text = ShortDescEntry.Text = FullDescriptionEntry.Text = string.Empty;
                            Navigation.PopAsync();
                        }
                    }

                    else
                    {
                        try
                        {
                            var item = _item;
                            item.Name = nameEntry.Text.ReplaceWhiteSpaces();
                            item.ShortDescription = ShortDescEntry.Text.ReplaceWhiteSpaces();
                            item.FullDescription = FullDescriptionEntry.Text.ReplaceWhiteSpaces();
                            ArticleService.UpdateArticle(item);
                            nameEntry.Text = ShortDescEntry.Text = FullDescriptionEntry.Text = string.Empty;
                            Navigation.PopAsync();
                        }
                        catch (SqliteException exception)
                        {
                            DisplayAlert("Ошибка!", "Что-то пошло не так! Сообщение: " + exception, "Понятно");
                        }
                    }
                }
            }
        }

        private async void Button_OnClicked(object sender, EventArgs e) //открывает окно добавления нового фото
        {
            await Navigation.PushAsync(new AddPhoto(_item.Id));
        }


        private async void ButtonDelete_OnClicked(object sender, EventArgs e) // открывает лист фоток для удаления
        {
            await Navigation.PushAsync(new DeletePhoto(_item.Id));
        }
    }
}