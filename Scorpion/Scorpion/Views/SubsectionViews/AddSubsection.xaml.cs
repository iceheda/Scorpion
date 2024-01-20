using System;
using Scorpion.Models;
using Scorpion.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.SubsectionViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSubsection
    {
        private readonly int _id;
        private readonly bool _isEdit = true;
        private readonly Subsection _item;
        private bool _wasClicked; //защита от двойного нажатия по button

        public AddSubsection(Subsection item, int SectionId)
        {
            InitializeComponent();

            //  ToolbarItem tb = new ToolbarItem()
            //  {
            //      Text = "Сохранить",
            //      Order = ToolbarItemOrder.Default,
            //      Priority = 0,
            //
            //  };
            //  ToolbarItems.Add(tb);

            _id = SectionId;

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
            await Navigation.PopAsync();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nameEntry.Text))
            {
                if (_wasClicked == false)
                {
                    _wasClicked = true;
                    if (_isEdit == false) //если это не редактирование, то добавление {..}
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
                            {
                                var item = new Subsection
                                {
                                    Name = nameEntry.Text.ReplaceWhiteSpaces(),
                                    SectionId = _id
                                };
                                SubsectionService.SaveSubsection(item);
                                Navigation.PopAsync(true);
                            }
                        }
                        catch (Exception exp)
                        {
                            DisplayAlert("Ошибка", "Подробности: " + exp, "Отменить");
                        }

                    else
                        try
                        {
                            _item.Name = nameEntry.Text.ReplaceWhiteSpaces();
                            SubsectionService.UpdateSubsection(_item);
                            Navigation.PopAsync(true);
                        }

                        catch (Exception exp)
                        {
                            DisplayAlert("Ошибка", "Подробности: " + exp, "Отменить");
                        }
                }
            }
            else
            {
                ToastService.ToastShow("Введите название");
            }
        }

    }
}