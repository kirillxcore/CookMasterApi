using Android.App;
using Android.Widget;
using Android.OS;
using System;
using CookAndroid.Repo;

namespace CookAndroid
{
    [Activity(Label ="Settings", Theme = "@android:style/Theme.NoTitleBar")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Settings);

            Button buttonCreateMenu = FindViewById<Button>(Resource.Id.SettingsApply);
            EditText textSettngsHostAddress = FindViewById<EditText>(Resource.Id.SettngsHostAddress);
            textSettngsHostAddress.Text = APIClient.HostName;
            buttonCreateMenu.Click += (object sender, EventArgs e) =>
            {
                APIClient.HostName = textSettngsHostAddress.Text;
                this.Finish();
            };
        }
    }
}

