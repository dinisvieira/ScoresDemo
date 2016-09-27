using System;
using System.IO;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using XamSvg;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScoresDemo.Droid
{
    public class MatchesAdapter : BaseAdapter<string>
    {
        Match[] matches;
        Activity context;

        public MatchesAdapter(Activity context, Match[] items) : base()
        {
            this.context = context;
            this.matches = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override string this[int position]
        {
            get { return matches[position].VenueName; }
        }
        public override int Count
        {
            get { return matches.Length; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = matches[position];
            View view = context.LayoutInflater.Inflate(Resource.Layout.MatchListItem, null); //we are not reusing the convertView but should on a real app

            view.FindViewById<TextView>(Resource.Id.homeName).Text = item.HomeName;
            view.FindViewById<TextView>(Resource.Id.awayName).Text = item.AwayName;
            view.FindViewById<TextView>(Resource.Id.homeGoals).Text = item.HomeGoals.ToString();
            view.FindViewById<TextView>(Resource.Id.awayGoals).Text = item.AwayGoals.ToString();

            view.FindViewById<TextView>(Resource.Id.venueName).Text = item.VenueName;

            view.FindViewById<TextView>(Resource.Id.date).Text = item.StartTime.ToString("dd-MM-yyyy");

            Android.Net.Uri uriHomeImg = Android.Net.Uri.Parse(item.HomeShirtImgUrl);
            view.FindViewById<ImageView>(Resource.Id.homeImage).SetImageURI(uriHomeImg);

            var homeImage = view.FindViewById<ImageView>(Resource.Id.homeImage);
            LoadSvg(item.HomeShirtImgUrl, homeImage);

            var awayImage = view.FindViewById<ImageView>(Resource.Id.awayImage);
            LoadSvg(item.AwayShirtImgUrl, awayImage);

            return view;
        }

        //Inefficient way to get svg image to bitmap during list loading (Do not use, just for demo purposes)
        private async void LoadSvg(string svgUrl, ImageView awayImage)
        {
            try
            {
                var awayBitmap = await LoadSvgString(svgUrl);
                awayImage.SetImageBitmap(awayBitmap);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        //Load svg string from url
        private async Task<Bitmap> LoadSvgString(string svgUrl)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-crowdscores-api-key", "91e57182c3954dc68a2e184c30271b4a");
                var result = await client.GetAsync(svgUrl);
                var svgString = await result.Content.ReadAsStringAsync();

                var image = SvgFactory.GetBitmap(new StringReader(svgString), 50, 50);
                return image;
            }
        }
    }
}