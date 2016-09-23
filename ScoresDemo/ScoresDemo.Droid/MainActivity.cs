using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ScoresDemo.Droid
{
	[Activity (Label = "ScoresDemo.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.fetchMatchesButton);
            EditText editText = FindViewById<EditText>(Resource.Id.numberDaysEditText);

            button.Click += async (sender, args) =>
            {
                int numberDays = 5;
                int.TryParse(editText.Text, out numberDays);

			    var matches = await ScoresSource.GetLastMatches(numberDays);
                System.Diagnostics.Debug.WriteLine(matches.Count);
			};
		}
	}
}