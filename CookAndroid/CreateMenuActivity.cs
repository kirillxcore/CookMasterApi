﻿using Android.App;
using Android.Net;
using Android.OS;
using Android.Views;
using Android.Widget;
using CookAndroid.Repo;
using CookMasterApiModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
namespace CookAndroid
{
    [Activity(Label ="Create new menu.", Theme = "@android:style/Theme.NoTitleBar")]
    public class CreateMenuActivity : Activity
    {
        List<DishWrapper> items;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CreateMenu);

            var buttonSaveMenu = FindViewById<Button>(Resource.Id.CreateMenuSave);
            buttonSaveMenu.Click += ButtonSaveMenu_Click;
            buttonSaveMenu.Visibility = ViewStates.Invisible;

            var menuAvailable = FindViewById<ListView>(Resource.Id.CreateMenuList);
            menuAvailable.ItemClick += MenuAvailable_ItemClick;

            var labelHeader = FindViewById<TextView>(Resource.Id.CreateMenuHeader);
            labelHeader.Text = "Loading dishes...";

            APIClient.GetDishes().ContinueWith(task =>
            {
                var result = task.Result;
                this.RunOnUiThread(() =>
                {
                    if (result == null)
                    {
                        labelHeader.Text = "Faild to load dishes.";
                        return;
                    }

                    this.items = task.Result.Select(a => new DishWrapper
                    {
                        Id = a.Id,
                        IsSelected = false,
                        ImageUrl = a.ImageUrl,
                        ImageUrlAlt = a.ImageUrlAlt,
                        IsVegan = a.IsVegan,
                        Name = a.Name,
                        CategoryId = a.CategoryId,
                        Description = a.Description
                    }).ToList();

                    menuAvailable.Adapter = new CreateMenuAdapter(this, items);
                    buttonSaveMenu.Visibility = ViewStates.Visible;
                    labelHeader.Text = "Create menu";
                });
            });
        }

        private void ButtonSaveMenu_Click(object sender, System.EventArgs e)
        {
            APIClient.Publish(items.Where(a => a.IsSelected).Select(a => a.Id).ToList()).ContinueWith(a =>
            {
                var result = a.Result;
                this.RunOnUiThread(() =>
                {
                    if (result)
                    {
                        Toast.MakeText(this, "Published succesfully", ToastLength.Short);
                    }
                    else
                    {
                        Toast.MakeText(this, "Publish failed", ToastLength.Short);
                    }
                });
            });
            this.Finish();
        }

        private void MenuAvailable_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = items[e.Position];
            item.IsSelected = !item.IsSelected;
            e.View.FindViewById<TextView>(Resource.Id.CreateMenuItemIsSelected).Text = item.IsSelected ? "X" : "-";
        }

        public class DishWrapper : DishItem
        {
            public bool IsSelected { get; set; }
        }

        public class CreateMenuAdapter : BaseAdapter<DishWrapper>
        {
            List<DishWrapper> items;
            Activity context;

            public CreateMenuAdapter(Activity context, List<DishWrapper> items) : base()
            {
                this.context = context;
                this.items = items;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override DishWrapper this[int position]
            {
                get { return items[position]; }
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                if (view == null)
                {
                    view = context.LayoutInflater.Inflate(Resource.Layout.CreateMenuItem, null);
                }


                view.FindViewById<TextView>(Resource.Id.CreateMenuItemName).Text = items[position].Name;
                view.FindViewById<TextView>(Resource.Id.CreateMenuItemDescription).Text = items[position].Description;
                view.FindViewById<TextView>(Resource.Id.CreateMenuItemIsSelected).Text = items[position].IsSelected ? "X" : "-";
                view.FindViewById<ImageView>(Resource.Id.CreateMenuItemImage).SetUrlAsync(context, items[position].ImageUrlAlt, context.Resources.GetDrawable(Resource.Drawable.ico));
                return view;
            }
        }
    }
}

