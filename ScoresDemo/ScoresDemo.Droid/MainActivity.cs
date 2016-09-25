using System;
using System.Collections.ObjectModel;
using System.Linq;
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

		protected override async void OnCreate (Bundle bundle)
		{

            XamSvg.Setup.InitSvgLib();

            base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.fetchMatchesButton);
            ListView listView = FindViewById<ListView>(Resource.Id.matchesListView);

            //Calls the Shared Portable Class Library to get a list with all available meme's.
            ObservableCollection<Competition> competitions = await ScoresSource.GetCompetitions();

            Spinner competitionsSpinner = FindViewById<Spinner>(Resource.Id.competitionsSpinner);

            var orderedCompetitionsArray = competitions.OrderBy(m => m.Id).ToArray();

            //Set the list of memes to our Spinner and enable it
            var adapter = new CompetitionsAdapter(this, orderedCompetitionsArray);
            competitionsSpinner.Adapter = adapter;

            button.Click += async (sender, args) =>
            {
			    var matches = await ScoresSource.GetLastCompetitionMatches(competitionsSpinner.SelectedItemId.ToString());
                System.Diagnostics.Debug.WriteLine(matches.Count);

                var orderedMatchesArray = matches.OrderByDescending(m => m.StartTime).ToArray();
                
                listView.Adapter = new MatchesAdapter(this, orderedMatchesArray);
            };
		}
	}
}