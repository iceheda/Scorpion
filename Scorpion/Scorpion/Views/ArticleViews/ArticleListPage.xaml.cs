using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Scorpion.Models;
using Scorpion.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.ArticleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticleListPage : ContentPage
    {
        private readonly int _id;
        private Article listviewItem = new();

        public ArticleListPage(Subsection subsection)
        {
            InitializeComponent();
            _id = subsection.Id;
            Title = subsection.Name ?? "Подраздел";

        }

        private void ArticleListPage_OnAppearing(object sender, EventArgs e)
        {
            Refresh();
            ToolbarVisible();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (EditModeService.IsEditMode == true)
            {
                var grid = sender as Grid;

                if (grid.BindingContext != listviewItem)
                {
                    ListViewArticle.SelectedItem = grid.BindingContext;
                    listviewItem = (ListViewArticle.SelectedItem as ArticleWithPhoto).Article;
                }
                else
                {
                    ListViewArticle.SelectedItem = null;
                    listviewItem = null;
                }
            }
            else
            {
                var grid = sender as Grid;
                ListViewArticle.SelectedItem = grid.BindingContext;

                Navigation.PushAsync(new ArticlePage((ListViewArticle.SelectedItem as ArticleWithPhoto).Article));
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e) //double
        {
            var grid = sender as Grid;
            ListViewArticle.SelectedItem = grid.BindingContext;

            Navigation.PushAsync(new ArticlePage((ListViewArticle.SelectedItem as ArticleWithPhoto).Article));
        }

        public void ToolbarVisible()
        {
            ToolbarItems.Clear();

            if (EditModeService.IsEditMode == true)
            {
                ToolbarItem addItem = new()
                {
                    Text = "Добавить",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 0
                };
                ToolbarItem editItem = new()
                {
                    Text = "Изменить",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 1
                };
                ToolbarItem deleteItem = new()
                {
                    Text = "Удалить",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 2
                };
                ToolbarItems.Add(addItem);
                ToolbarItems.Add(editItem);
                ToolbarItems.Add(deleteItem);

                addItem.Clicked += async (s, e) =>
                {
                    await Navigation.PushAsync(new AddArticle(null, _id));
                };

                editItem.Clicked += async (s, e) =>
                {
                    if (ListViewArticle.SelectedItem != null)
                    {
                        var item = ListViewArticle.SelectedItem as Article; //инициализируем item как выбранную строку в листвью как Section

                        await Navigation.PushAsync(new AddArticle(item, _id)); //передаем item на новую страницу для изменения
                    }
                };

                deleteItem.Clicked += async (s, e) =>
                {
                    try
                    {
                        if (ListViewArticle.SelectedItem is Article item)
                        {
                            // var result = DisplayAlert("Подтвердить действие", "Это действие удалит данную заметку. Продолжить?", "Да", "Нет");
                            // if (result.Result != true) return;
                            var check = await DisplayAlert("Внимание", "Это действие удалит выбранную статью. Продолжить?",
                                "Да", "Нет");
                            if (check)
                            {
                                ArticleService.DeleteArticle(item);
                                Refresh();
                            }
                        }
                        else
                        {
                            ToastService.ToastShow("Выберите удаляемую статью.");
                        }
                    }
                    catch (SqliteException exception)
                    {
                        await DisplayAlert("Ошибка!", "Что-то пошло не так! Сообщение: " + exception, "Понятно");
                    }

                };
            }
        }

        private void Refresh()
        {
            try
            {
                ListViewArticle.ItemsSource = null;
                ListViewArticle.ItemsSource = ArticleService.GetArticlesWithPhoto(_id);
                ListViewArticle.SelectedItem = null;
                //ListViewArticle.ItemsSource = null;
                //List<ArticleList> ArticleList = new() { new() { articles = ArticleService.GetSomeArticleList(_id), photo = PhotoService.GetFirstArticleImage(_id) } };
                //ListViewArticle.ItemsSource = ArticleList;
                //ListViewArticle.SelectedItem = null;
            }
            catch (SqliteException exception)
            {
                DisplayAlert("Ошибка!", "Что-то пошло не так! Сообщение: " + exception, "Понятно");
            }
        }
    }
}