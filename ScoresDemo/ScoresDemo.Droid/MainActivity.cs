using System.Collections.ObjectModel;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.OS;

namespace ScoresDemo.Droid
{
	[Activity (Label = "ScoresDemo.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override async void OnCreate (Bundle bundle)
		{
            //Initialize SVG Library
            XamSvg.Setup.InitSvgLib();

            base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our buttom, listvew and spinner from the layout resource,
			Button button = FindViewById<Button> (Resource.Id.fetchMatchesButton);
            ListView listView = FindViewById<ListView>(Resource.Id.matchesListView);
            Spinner competitionsSpinner = FindViewById<Spinner>(Resource.Id.competitionsSpinner);

            //Calls the Shared Portable Class Library to get a list with all available competitions
            ObservableCollection<Competition> competitions = await ScoresSource.GetCompetitions();

            //Order the competitions by Id
            var orderedCompetitionsArray = competitions.OrderBy(m => m.Id).ToArray();

            //Set the competitions list on the Spinner Adapter
            var adapter = new CompetitionsAdapter(this, orderedCompetitionsArray);
            competitionsSpinner.Adapter = adapter;

            //Set a click event for the Fetch Matches button
            button.Click += async (sender, args) =>
            {
                //Get the matches of the selected competition (using the PCL)
			    var matches = await ScoresSource.GetLastCompetitionMatches(competitionsSpinner.SelectedItemId.ToString());

                //Order the matches by date
                var orderedMatchesArray = matches.OrderByDescending(m => m.StartTime).ToArray();
                
                //Set the matches list on the ListView Adapter
                listView.Adapter = new MatchesAdapter(this, orderedMatchesArray);
            };
		}
	}
}