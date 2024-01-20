using Scorpion.Services;
using Scorpion.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion
{
    public partial class App
    {

        public App()
        {
            InitializeComponent();
            SqliteAccess.CheckAccessToStorage();
            OptionService.CheckProperties();
            OptionService.GetProperties();
            CheckIfDbExists();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }


        public static string GetDatabasePath()
        {
            return PathService.GetLocalAppPath;
        }

        public static void CheckIfDbExists()
        {
            if (!File.Exists(GetDatabasePath()))
                GetDataService.GetData().Wait();

            // SqliteAccess.CreateDb();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
    public static class StringExtension
    {
        public static string ReplaceWhiteSpaces(this string str)
        {
            while (str.Contains("  ")) str = str.Replace("  ", " ");
            return str.Trim();
        }

    }
}
