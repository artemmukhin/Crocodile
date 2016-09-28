using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace Crocodile
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/AppBaseTheme",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameActivity : Activity
    {
        private TextView wordTextView;
        private Stack<string> words;
        private List<Category> categories;
        private string[] wordsList; // for reuse

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Game);

            ImageButton nextWordButton = FindViewById<ImageButton>(Resource.Id.NextWordButton);
            nextWordButton.Click += new EventHandler(NextWord_Clicked);

            wordTextView = FindViewById<TextView>(Resource.Id.textView1);
            categories = MainActivity.Categories;
            words = WordList.GenerateWords(categories);
            wordsList = words.ToArray();
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
                alert.SetMessage("Классический Крокодил. Для игры необходимо несколько человек. Ведущему " +
                                 "необходимо с помощью жестов и мимики объяснить слово остальным игрокам." +
                                 "Игрок, отгадавший слово первым, становится новым ведущим.\n\n Приятной игры!");
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

        protected void NextWord_Clicked(Object sender, EventArgs e)
        {
            if (words.Count != 0)
            {
                wordTextView.Text = words.Pop();
            }
            else
            {
                words = WordList.GenerateWords(categories);
                wordTextView.Text = words.Pop();
            }
        }
    }
}

