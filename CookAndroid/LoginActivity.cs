using Android.App;
using Android.Widget;
using Android.OS;
using System;
using CookAndroid.Repo;
using Android.Text;
using Android.Content;

namespace CookAndroid
{
    [Activity(Label = "Login", Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            EditText editLogin = FindViewById<EditText>(Resource.Id.EditLogin);
            EditText editPassword = FindViewById<EditText>(Resource.Id.EditPassword);
            Button buttonLogin = FindViewById<Button>(Resource.Id.ButtonLogin);
            TextView labelError = FindViewById<TextView>(Resource.Id.LabelError);

            editLogin.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                labelError.Text = "";
            };

            editPassword.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                labelError.Text = "";
            };

            buttonLogin.Click += (object sender, EventArgs e) =>
            {
                var isLoggedIn = APIClient.Login(editLogin.Text, editPassword.Text);
                if (!isLoggedIn)
                {
                    labelError.Text = "Login incorrect.";
                    return;
                }

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }
    }
}

