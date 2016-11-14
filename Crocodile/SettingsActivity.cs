using Android.App;
using Android.Preferences;
using Android.OS;
using Android.Content.PM;

namespace Crocodile
{
    [Activity(Label = "Настройки", Icon = "@drawable/Icon", Theme = "@style/AppBaseTheme",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsActivity : PreferenceActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Menu.preferences);
        }

        protected override void OnStop()
        {
            base.OnStop();
            MainActivity.Activity.UpdateAdultSettings();
        }
    }
}
