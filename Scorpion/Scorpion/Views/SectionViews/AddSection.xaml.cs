using System;
using Scorpion.Models;
using Scorpion.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.SectionViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSection : ContentPage
    {
        private readonly bool _isEdit = true;
        private readonly Section _item;
        private bool _wasClicked; //защита от двойного нажатия по button

        public AddSection(Section item)
        {
            InitializeComponent();
            if (item == null)
            {
                _isEdit = false;
            }
            else
            {
                _item = item;
                nameEntry.Text = item.Name;
            }
        }

        private async void Cancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        private async void Save_OnClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nameEntry.Text))
            {
                if (_wasClicked == false)
                {
                    _wasClicked = true;
                    if (_isEdit == false)
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
                            {
                                var item = new Section
                                {
                                    Name = nameEntry.Text.ReplaceWhiteSpaces()
                                };
                                SectionService.SaveSection(item);
                                await Navigation.PopModalAsync(true);
                            }
                        }
                        catch (Exception exp)
                        {
                            await DisplayAlert("Ошибка: ", "Ок " + exp, "Отменить");
                        }

                    else
                        try
                        {
                            _item.Name = nameEntry.Text.ReplaceWhiteSpaces();
                            SectionService.UpdateSection(_item);
                            await Navigation.PopModalAsync(true);
                            // ToastService.ToastShow("Название раздела было изменено на " + nameEntry.Text);
                        }

                        catch (Exception exp)
                        {
                            await DisplayAlert("Ошибка: ", "Ок" + exp, "Отменить");
                        }
                }
            }
        }
    }
}