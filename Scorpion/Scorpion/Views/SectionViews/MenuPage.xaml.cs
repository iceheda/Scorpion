using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using ReactiveUI;
using Scorpion.Messages;
using Scorpion.Models;
using Scorpion.Services;
using Scorpion.Views.MainPages;
using Scorpion.Views.SubsectionViews;
using Xamarin.Forms;

namespace Scorpion.Views.SectionViews
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        private Section _lvItem = new Section();

        public MenuPage()
        {
            InitializeComponent();
            EditModeService.EditModeChanged += EditMode_EMChanged;
        }

        private void EditMode_EMChanged(bool value)
        {
            OptionService.SaveProperties();
            RefreshMode();
            Refresh();
        }

        private async void ButtonAdd_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddSection(null));
        }

        private async void ButtonEdit_OnClicked(object sender, EventArgs e)
        {
            if (ListViewMenu.SelectedItem != null)
            {
                var item = ListViewMenu.SelectedItem as Section;
                await Navigation.PushModalAsync(new AddSection(item));
            }
            else
            {
                ToastService.ToastShow("Сначала выберите редактируемую секцию");
            }
        }

        private async void ButtonDelete_OnClicked(object sender, EventArgs e)
        {
            if (ListViewMenu.SelectedItem == null)
            {
                ToastService.ToastShow("Сначала выберите удаляемую секцию");
                return;
            }

            try
            {
                var check = await DisplayAlert("Внимание",
                    "Вы пытаетесь удалить выбранный раздел. Это означает, что вместе с ним удалятся все подразделы и статьи. Продолжить?",
                    "Да", "Нет");

                if (!check) return;

                var secondCheck =
                    await DisplayAlert("Защита от случайного нажатия", "Продолжить?", "Да", "Нет");

                if (!secondCheck) return;

                var item = ListViewMenu.SelectedItem as Section;
                SectionService.DeleteSection(item);
                MessageBus.Current.SendMessage(new OpenPageMessage(new WelcomePage()));
            }
            catch (SqliteException exp)
            {
                await DisplayAlert("Ошибка!", "Что-то пошло не так! Сообщение: " + exp, "Понятно");
            }
            finally
            {
                Refresh();
            }

        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e) //double tab
        {
            if (EditModeService.IsEditMode)
            {
                if (sender is Grid grid && grid.BindingContext != _lvItem)
                {
                    ListViewMenu.SelectedItem = grid.BindingContext;
                    _lvItem = ListViewMenu.SelectedItem as Section;
                }

                if (ListViewMenu.SelectedItem is Section section)
                {
                    MessageBus.Current.SendMessage(new OpenPageMessage(new SubsectionListPage(section, section.Id)));
                    ListViewMenu.SelectedItem = null;
                    _lvItem = null;
                }
            }
            else
            {
                if (sender is Grid grid)
                    ListViewMenu.SelectedItem = grid.BindingContext;

                if (ListViewMenu.SelectedItem is Section section)
                {
                    MessageBus.Current.SendMessage(new OpenPageMessage(new SubsectionListPage(section, section.Id)));
                    ListViewMenu.SelectedItem = null;
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            if (EditModeService.IsEditMode)
            {
                if (sender is Grid grid && grid.BindingContext != _lvItem)
                {
                    ListViewMenu.SelectedItem = grid.BindingContext;
                    _lvItem = ListViewMenu.SelectedItem as Section;
                }
                else
                {
                    ListViewMenu.SelectedItem = null;
                    _lvItem = null;
                }
            }

            else
            {
                if (sender is Grid grid)
                    ListViewMenu.SelectedItem = grid.BindingContext;

                if (ListViewMenu.SelectedItem is Section section)
                {
                    MessageBus.Current.SendMessage(new OpenPageMessage(new SubsectionListPage(section, section.Id)));
                    ListViewMenu.SelectedItem = null;
                }
            }

        }


        private void RefreshMode()
        {
            if (EditModeService.IsEditMode == false)
            {
                ButtonAdd.IsVisible = false;
                ButtonEdit.IsVisible = false;
                ButtonDelete.IsVisible = false;
            }
            else
            {
                ButtonAdd.IsVisible = true;
                ButtonEdit.IsVisible = true;
                ButtonDelete.IsVisible = true;
            }
        }

        private void Refresh()
        {
            ListViewMenu.ItemsSource = null;
            ListViewMenu.ItemsSource = SectionService.GetItemList();
            ListViewMenu.SelectedItem = null;
        }

        private void ButHome_OnClicked(object sender, EventArgs e)
        {
            MessageBus.Current.SendMessage(new OpenPageMessage(new WelcomePage()));
        }

        private void MenuPage_OnAppearing(object sender, EventArgs e)
        {
            Refresh();
            RefreshMode();
        }
    }
}