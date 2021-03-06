﻿using Android.App;
using Android.Widget;
using Android.OS;
using System;
using CookAndroid.Repo;
using Android.Text;
using Android.Content;

namespace CookAndroid
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/logo", Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            EditText editLogin = FindViewById<EditText>(Resource.Id.LoginLogin);
            EditText editPassword = FindViewById<EditText>(Resource.Id.LoginPassword);
            Button buttonLogin = FindViewById<Button>(Resource.Id.LoginButton);
            TextView labelError = FindViewById<TextView>(Resource.Id.LoginError);
            Button buttonSettings = FindViewById<Button>(Resource.Id.MainViewSettings);

            buttonSettings.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
            };

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
                    labelError.Text = "User name or password is incorrect";
                    return;
                }

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }
    }
}

