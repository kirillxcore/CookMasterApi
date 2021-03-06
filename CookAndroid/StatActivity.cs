﻿using Android.App;
using Android.Widget;
using Android.OS;
using CookAndroid.Repo;
using System.Collections.Generic;
using CookMasterApiModel;
using Android.Views;
using System.Linq;

[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
namespace CookAndroid
{
    [Activity(Label = "Statistic", Theme = "@android:style/Theme.NoTitleBar")]
    public class StatActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Stat);

            var days = Intent.GetIntExtra("days", 0);
            
            var labelHeader = FindViewById<TextView>(Resource.Id.StatHeader);
            labelHeader.Text = "Loading statistic...";

            var menuAvailable = FindViewById<ListView>(Resource.Id.StatList);
            menuAvailable.Clickable = false;

            APIClient.Stat(days).ContinueWith(task =>
            {
                var result = task.Result;
                this.RunOnUiThread(() =>
                {
                    if (result == null)
                    {
                        labelHeader.Text = "Faild to load statistic.";
                        return;
                    }

                    var items = result.Select(a => new DishWrapper
                    {
                        Id = a.Item.Id,
                        Count = a.Count,
                        ImageUrl = a.Item.ImageUrl,
                        ImageUrlAlt = a.Item.ImageUrlAlt,
                        IsVegan = a.Item.IsVegan,
                        Name = a.Item.Name,
                        CategoryId = a.Item.CategoryId,
                        Description = a.Item.Description
                    }).ToList();

                    labelHeader.Text = "Statistic for " + days + " days.";
                    menuAvailable.Adapter = new StatAdapter(this, items);
                });
            });
        }

        public class DishWrapper : DishItem
        {
            public int Count { get; set; }
        }

        public class StatAdapter : BaseAdapter<DishWrapper>
        {
            List<DishWrapper> items;
            Activity context;

            public StatAdapter(Activity context, List<DishWrapper> items) : base()
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
                    view = context.LayoutInflater.Inflate(Resource.Layout.StatItem, null);
                }

                view.FindViewById<TextView>(Resource.Id.StatItemName).Text = items[position].Name;
                view.FindViewById<TextView>(Resource.Id.StatItemDescription).Text = items[position].Description;
                view.FindViewById<TextView>(Resource.Id.StatItemCount).Text = items[position].Count.ToString();
                view.FindViewById<ImageView>(Resource.Id.StatItemImage).SetUrlAsync(context, items[position].ImageUrlAlt, context.Resources.GetDrawable(Resource.Drawable.ico));
                return view;
            }
        }
    }
}

