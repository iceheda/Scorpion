using Xamarin.Forms;

namespace Scorpion.Services
{
    public class ThemeService
    {
        public delegate void ThemeModeHandler(OSAppTheme value);

        private static readonly OSAppTheme _themeMode = Application.Current.RequestedTheme;
        public static bool _themeBool;

        public static bool ThemeBool
        {
            get => _themeBool;
            set => _themeBool = value;
        }

        public static OSAppTheme ThemeMode
        {
            get => _themeMode;
            set
            {
                ThemeModeChanged?.Invoke(value);
                Application.Current.UserAppTheme = value;
            }
        }

        public static event ThemeModeHandler ThemeModeChanged;
    }
}