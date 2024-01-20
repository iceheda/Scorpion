using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scorpion.Services
{
    public class OptionService
    {
        public static void CheckProperties()
        {
            if (!Application.Current.Properties.ContainsKey("editMode"))
            {
                Application.Current.Properties.Add("editMode", 0);
            }
        }

        public static void SaveProperties()
        {
            if (App.Current.Properties.ContainsKey("editMode"))
            {
                if (EditModeService.IsEditMode)
                {
                    Application.Current.Properties["editMode"] = 1;
                }
                else
                {
                    Application.Current.Properties["editMode"] = 0;
                }
            }
        }

        public static void GetProperties()
        {
            EditModeService.IsEditMode = Convert.ToBoolean(App.Current.Properties["editMode"]);
        }

    }
}