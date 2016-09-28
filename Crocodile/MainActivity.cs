using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;
using Android.Content.PM;


namespace Crocodile
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon",
        Theme = "@style/AppBaseTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private static MainActivity activity;
        public static MainActivity Activity { get { return activity; } }

        private Stack<string> words;

        private static List<Category> categories;
        public static List<Category> Categories { get { return categories; } }

        ISharedPreferences prefs;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            ImageButton newGameButton = FindViewById<ImageButton>(Resource.Id.NewGameButton);
            newGameButton.Click += new EventHandler(NewGame_Clicked);
            ImageButton instructionButton = FindViewById<ImageButton>(Resource.Id.InstructionButton);
            instructionButton.Click += new EventHandler(Instruction_Clicked);

            UpdateAdultSettings();

            categories = new List<Category>() { Category.profession, Category.people, Category.other,
                                                Category.nature, Category.conception, Category.character};
            activity = this;
        }

        public void UpdateAdultSettings()
        {
            bool isAdult = prefs.GetBoolean("Adult", false);
            if (isAdult)
            {
                FindViewById<CheckBox>(Resource.Id.checkBox7).Visibility = ViewStates.Visible;
                FindViewById<CheckBox>(Resource.Id.checkBox7).Enabled = true;
            }
            else
            {
                FindViewById<CheckBox>(Resource.Id.checkBox7).Visibility = ViewStates.Invisible;
                FindViewById<CheckBox>(Resource.Id.checkBox7).Enabled = false;
                FindViewById<CheckBox>(Resource.Id.checkBox7).Checked = false;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.action_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.menuHelp)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Об игре");
                alert.SetMessage("Авторы:\n" +
                                 "Артем Мухин (ortem73@gmail.com)\n" +
                                 "Екатерина Буланина (fearlesspf0@gmail.com)");
                alert.SetPositiveButton("OK", (senderAlert, args) => { });
                Dialog dialog = alert.Create();
                dialog.Show();
                return true;
            }

            else if (id == Resource.Id.menuSettings)
            {
                //Toast.MakeText(this, "настройки", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected void Instruction_Clicked(Object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Как играть");
            alert.SetMessage("Классический Крокодил. Для игры необходимо несколько человек. Ведущему " +
                             "необходимо с помощью жестов и мимики объяснить слово остальным игрокам. " +
                             "Игрок, отгадавший слово первым, становится новым ведущим.\n\n Приятной игры!");
            alert.SetPositiveButton("OK", (senderAlert, args) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        protected void NewGame_Clicked(Object sender, EventArgs e)
        {
            UpdateAdultSettings();

            categories = new List<Category>();
            if (FindViewById<CheckBox>(Resource.Id.checkBox1).Checked)
                categories.Add(Category.profession);
            if (FindViewById<CheckBox>(Resource.Id.checkBox2).Checked)
                categories.Add(Category.people);
            if (FindViewById<CheckBox>(Resource.Id.checkBox3).Checked)
                categories.Add(Category.nature);
            if (FindViewById<CheckBox>(Resource.Id.checkBox4).Checked)
                categories.Add(Category.conception);
            if (FindViewById<CheckBox>(Resource.Id.checkBox5).Checked)
                categories.Add(Category.character);
            if (FindViewById<CheckBox>(Resource.Id.checkBox6).Checked)
                categories.Add(Category.other);
            if (FindViewById<CheckBox>(Resource.Id.checkBox7).Checked)
                categories.Add(Category.adult);

            if (categories.Count != 0)
            {
                words = WordList.GenerateWords(categories);
                var intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
            }
        }
    }
}

