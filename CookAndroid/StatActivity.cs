using Android.App;
using Android.Widget;
using Android.OS;
using CookAndroid.Repo;
using System.Collections.Generic;
using CookMasterApiModel;
using Android.Views;
using System.Linq;

namespace CookAndroid
{
    [Activity(Label = "Statistic", Theme = "@android:style/Theme.NoTitleBar")]
    public class StatActivity : Activity
    {
        List<DishWrapper> items;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Stat);

            var days = Intent.GetIntExtra("days", 0);
            
            TextView labelHeader = FindViewById<TextView>(Resource.Id.LabelStatHeader);
            labelHeader.Text = "Statistic for " + days + " days.";


            this.items = APIClient.Stat(days).Select(a => new DishWrapper
            {
                Id = a.Item.Id,
                Count = a.Count,
                ImageUrl = a.Item.ImageUrl,
                IsVegan = a.Item.IsVegan,
                Name = a.Item.Name
            }).ToList();

            var menuAvailable = FindViewById<ListView>(Resource.Id.StatAvailable);
            menuAvailable.Adapter = new StatAdapter(this, items);
            menuAvailable.FastScrollEnabled = true;
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
                    view = context.LayoutInflater.Inflate(Android.Resource.Layout.StatItem, null);
                }

                view.FindViewById<TextView>(Android.Resource.Id.NameStatText).Text = items[position].Name;
                view.FindViewById<TextView>(Android.Resource.Id.DescriptionStatText).Text = items[position].IsVegan ? "Vegan" : "All";
                view.FindViewById<TextView>(Android.Resource.Id.CountStatText).Text = items[position].Count.ToString();
                return view;
            }
        }
    }
}

