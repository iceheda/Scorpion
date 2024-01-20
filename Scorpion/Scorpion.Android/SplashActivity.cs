using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace Scorpion.Droid
{
    [Activity
        (
        Theme = "@style/Theme.Splash", 
        MainLauncher = true, 
        NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode,
        ScreenOrientation = ScreenOrientation.Portrait
        )]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    var startupWork = new Task(() => { SimulateStartup(); });
        //    startupWork.Start();
        //}
        //
        //private void SimulateStartup()
        //{
        //} бля че это за хуйню я тут насрал четыре года назад?
    }
}