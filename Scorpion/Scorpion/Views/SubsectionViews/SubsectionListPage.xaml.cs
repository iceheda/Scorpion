using System;
using Microsoft.Data.Sqlite;
using ReactiveUI;
using Scorpion.Messages;
using Scorpion.Models;
using Scorpion.Services;
using Scorpion.Views.ArticleViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.SubsectionViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubsectionListPage : ContentPage
    {
        private readonly int _id;
        private readonly Section _item;

        private Subsection LVItem = new Subsection();

        public SubsectionListPage(Section item, int id)
        {
            InitializeComponent();
            EditModeService.EditModeChanged += EditMode_EMChanged;
            _item = item;
            _id = id;
            Title = _item.Name ?? "Подразделы";
        }

        private void SubsectionListPage_OnAppearing(object sender, EventArgs e)
        {
            Refresh();
            ToolbarVisible();
        }

        private void EditMode_EMChanged(bool value)
        {
            ToolbarVisible();
            Refresh();
        }


        public void ToolbarVisible()
        {
            ToolbarItems.Clear();

            if (EditModeService.IsEditMode)
            {
                var addItem = new ToolbarItem()
                {
                    Text = "Добавить",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 0
                };
                var editItem = new ToolbarItem()
                {
                    Text = "Изменить",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 1
                };
                var deleteItem = new ToolbarItem()
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
                    await Navigation.PushAsync(new AddSubsection(null, _id));
                    ListViewSubsection.SelectedItem = null;
                };

                editItem.Clicked += async (s, e) =>
                {
                    if (ListViewSubsection.SelectedItem != null)
                    {
                        var item = ListViewSubsection.SelectedItem as Subsection;
                        await Navigation.PushAsync(new AddSubsection(item, _id));
                    }
                };

                deleteItem.Clicked += async (s, e) =>
                {
                    try
                    {
                        if (ListViewSubsection.SelectedItem is Subsection item)
                        {
                            var check = await DisplayAlert(
                                "Внимание",
                                "Это действие удалит выбранную подсекцию. Продолжить?",
                                "Да",
                                "Нет");
                            if (check)
                            {
                                SubsectionService.DeleteSubsection(item);
                                Refresh();
                            }
                        }
                    }
                    catch (SqliteException exception)
                    {
                        await DisplayAlert("Ошибка!", "Что-то пошло не так! Сообщение: " + exception, "Понятно");
                    }
                };
            }
        }
        //  private void Add_OnClicked(object sender, EventArgs e)
        //  {
        //      Navigation.PushModalAsync(new AddSubsection(null, _id));
        //      ListViewSubsection.SelectedItem = null;
        //  }

        //  private void Ed_OnClicked(object sender, EventArgs e)
        //  {
        //      if (ListViewSubsection.SelectedItem != null)
        //      {
        //          var item = ListViewSubsection.SelectedItem as Subsection;
        //          Navigation.PushModalAsync(new AddSubsection(item, _id));
        //      }
        //  }

        //  private async void Del_OnClicked(object sender, EventArgs e)
        //  {
        //      try
        //      {
        //          if (ListViewSubsection.SelectedItem is Subsection item)
        //          {
        //              var check = await DisplayAlert(
        //                  "Внимание",
        //                  "Это действие удалит выбранную подсекцию. Продолжить?",
        //                  "Да",
        //                  "Нет");
        //              if (check)
        //              {
        //                  SubsectionService.DeleteSubsection(item);
        //                  Refresh();
        //              }
        //          }
        //      }
        //      catch (SqliteException exception)
        //      {
        //          await DisplayAlert("Ошибка!", "Что-то пошло не так! Сообщение: " + exception, "Понятно");
        //      }
        //  }


        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e) //one tap
        {
            if (EditModeService.IsEditMode == true)
            {
                var grid = sender as Grid;

                if (grid.BindingContext != LVItem)
                {
                    ListViewSubsection.SelectedItem = grid.BindingContext;
                    LVItem = ListViewSubsection.SelectedItem as Subsection;
                }
                else
                {
                    ListViewSubsection.SelectedItem = null;
                    LVItem = null;
                }
            }
            else
            {
                var grid = sender as Grid;
                ListViewSubsection.SelectedItem = grid.BindingContext;

                if (ListViewSubsection.SelectedItem is Subsection subsection)
                    MessageBus.Current.SendMessage(new OpenPageMessage(new ArticleListPage(subsection)));
            }
        }

        private void TapGestureRecognizer_Tapped_Double(object sender, EventArgs e) // double tap 
        {
            var grid = sender as Grid;
            ListViewSubsection.SelectedItem = grid.BindingContext;

            if (ListViewSubsection.SelectedItem is Subsection subsection)
                MessageBus.Current.SendMessage(new OpenPageMessage(new ArticleListPage(subsection)));
        }


        private void Refresh()
        {
            try
            {
                ListViewSubsection.ItemsSource = null;
                ListViewSubsection.ItemsSource = SubsectionService.GetSomeSubsectionList(_id);
                ListViewSubsection.SelectedItem = null;
            }
            catch (SqliteException exception)
            {
                DisplayAlert("Ошибка!", "Что-то пошло не так! Сообщение: " + exception, "Понятно");
            }
        }
    }
}