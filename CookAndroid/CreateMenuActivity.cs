﻿using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CookAndroid.Repo;
using CookMasterApiModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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

            var buttonSaveMenu = FindViewById<Button>(Resource.Id.ButtonSaveMenu);
            buttonSaveMenu.Click += ButtonSaveMenu_Click;

            this.items = APIClient.GetDishes().Select(a => new DishWrapper
            {
                Id = a.Id,
                IsSelected = false,
                ImageUrl = a.ImageUrl,
                IsVegan = a.IsVegan,
                Name = a.Name
            }).ToList();

            var menuAvailable = FindViewById<ListView>(Resource.Id.MenuAvailable);
            menuAvailable.Adapter = new CreateMenuAdapter(this, items);
            menuAvailable.FastScrollEnabled = true;
            menuAvailable.ItemClick += MenuAvailable_ItemClick;
        }

        private void ButtonSaveMenu_Click(object sender, System.EventArgs e)
        {
            APIClient.Publish(items.Where(a => a.IsSelected).Select(a => a.Id).ToList());
            this.Finish();
        }

        private void MenuAvailable_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var t = items[e.Position];
            t.IsSelected = true;
            
        }

        public class DishWrapper : DishItem, INotifyPropertyChanged
        {
            private bool isSelected = false;
            public bool IsSelected
            {
                get
                {
                    return isSelected;
                }
                set
                {
                    isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
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

                view.FindViewById<TextView>(Resource.Id.NameText).Text = items[position].Name;
                view.FindViewById<TextView>(Resource.Id.DescriptionText).Text = items[position].IsVegan ? "Vegan" : "All";
                view.FindViewById<TextView>(Resource.Id.IsSelected).Text = items[position].IsSelected ? "X" : "-";
                return view;
            }
        }
    }
}

