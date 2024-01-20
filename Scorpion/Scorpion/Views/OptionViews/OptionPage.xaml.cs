using System;
using System.IO;
using Scorpion.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.OptionViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionPage : ContentPage
    {
        public OptionPage()
        {
            InitializeComponent();

            Sw1.IsToggled = EditModeService.IsEditMode;
            //ThemeSwitch.IsToggled = App.themeBool;

            // if (Application.Current.UserAppTheme == OSAppTheme.Dark)
            // {
            //     ThemeSwitch.IsToggled = true;
            //     App.themeBool = true;
            // }
            // else
            // {
            //     ThemeSwitch.IsToggled = false;
            //     App.themeBool = false;
            // }
        }
        // protected override void OnDisappearing()
        // {
        //     Navigation.PopModalAsync(false);
        // }
        // protected override bool OnBackButtonPressed()
        // {
        //     Navigation.PopAsync(false);
        //     return base.OnBackButtonPressed();
        // }
        // private void ThemeSwitch_OnToggled(object sender, ToggledEventArgs e)
        // {
        //     var thread = new Thread(Start);
        //     thread.Start();
        // }

        // private void Start()
        // {
        //   ThemeService.ThemeBool = ThemeSwitch.IsToggled;
        //   if (ThemeService.ThemeBool)
        //       Application.Current.UserAppTheme = OSAppTheme.Dark;
        //   else
        //       Application.Current.UserAppTheme = OSAppTheme.Light;
        // }

        private void EditSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            EditModeService.IsEditMode = Sw1.IsToggled;
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            try
            {
                GetDataService.GetDB().Wait();
                ToastService.ToastShow("База данных была успешно обновлена!");
            }
            catch (Exception)
            {
                DisplayAlert("Ошибка", "При попытки обновления базы данных произошла ошибка", "Ок");
            }
        }

        private async void Export_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(App.GetDatabasePath()))
                    try
                    {
                        File.Copy(App.GetDatabasePath(), PathService.GetRootPath);
                        ToastService.ToastShow("Файл базы данных успешно экспортирован в корневой каталог");
                    }
                    catch
                    {
                        var obj = await DisplayAlert("Внимание!",
                            "Файл базы данных уже существует в корневой папке. Заменить?", "Да", "Отмена");

                        if (obj)
                        {
                            File.Delete(PathService.GetExportDbPath);
                            File.Copy(App.GetDatabasePath(), PathService.GetRootPath);
                            ToastService.ToastShow("Файл базы данных успешно заменён в корневом каталоге");
                        }
                    }
            }
            catch (Exception exp)
            {
                await DisplayAlert("Ошибка!", "Что-то пошло не так: " + exp, "Ок");
            }
        }

        private async void Import_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(App.GetDatabasePath()))
                {
                    File.Delete(App.GetDatabasePath());
                    if (File.Exists(PathService.GetExportDbPath))
                    {
                        File.Copy(PathService.GetExportDbPath, PathService.GetLocalAppPath);
                        ToastService.ToastShow("База данных успешно импортирована. Перезапустите приложение");
                    }
                }
                else
                {
                    ToastService.ToastShow("Файла базы данных database.db не существует в корневом каталоге");
                }
            }
            catch (Exception exp)
            {
                await DisplayAlert("Ошибка!", "Что-то пошло не так: " + exp, "Ок");
            }
        }
    }
}