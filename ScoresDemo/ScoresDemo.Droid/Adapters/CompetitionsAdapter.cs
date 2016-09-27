using Android.App;
using Android.Views;
using Android.Widget;

namespace ScoresDemo.Droid
{
    public class CompetitionsAdapter : BaseAdapter<string>
    {
        Competition[] competitions;
        Activity context;

        public CompetitionsAdapter(Activity context, Competition[] items) : base()
        {
            this.context = context;
            this.competitions = items;
        }
        public override long GetItemId(int position)
        {
            return competitions[position].Id;
        }
        public override string this[int position]
        {
            get { return competitions[position].Name; }
        }
        public override int Count
        {
            get { return competitions.Length; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = competitions[position];
            View view = context.LayoutInflater.Inflate(Resource.Layout.CompetitionSpinnerItem, null); //we are not reusing the convertView but should on a real app

            view.FindViewById<TextView>(Resource.Id.textName).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.textRegion).Text = item.RegionName;

            return view;
        }
    }
}