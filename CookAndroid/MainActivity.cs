using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace CookAndroid
{
    [Activity(Label ="Main", Theme = "@android:style/Theme.NoTitleBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button buttonCreateMenu = FindViewById<Button>(Resource.Id.ButtonCreateMenu);
            Button buttonViewStatistic = FindViewById<Button>(Resource.Id.ButtonViewStatistic);
            Button buttonViewWeekStatistic = FindViewById<Button>(Resource.Id.ButtonViewWeekStatistic);

            buttonCreateMenu.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(CreateMenuActivity));
                StartActivity(intent);
            };

            buttonViewStatistic.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(StatActivity));
                intent.PutExtra("days", 1);
                StartActivity(intent);
            };

            buttonViewWeekStatistic.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(StatActivity));
                intent.PutExtra("days", 7);
                StartActivity(intent);
            };
        }
    }
}

