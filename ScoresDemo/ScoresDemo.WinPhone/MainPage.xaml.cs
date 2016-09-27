using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ScoresDemo.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //Calls the Shared Portable Class Library to get a list with all available competitions.
            ObservableCollection<Competition> competitions = await ScoresSource.GetCompetitions();

            //order the competitions by id
            var orderedCompetitions = competitions.OrderBy(m => m.Id);

            //set the Competitions Combox Box Items source to the list
            CompetitionsComboBox.ItemsSource = orderedCompetitions;

            //Create the tapped event for the Fetch Matches Button
            FetchMatchesButton.Tapped += async (sender, args) =>
            {
                //get the selected competition in the combobox
                var selComp = CompetitionsComboBox.SelectedValue as Competition;

                //get the matches for the selected competition
                var matches = await ScoresSource.GetLastCompetitionMatches(selComp.Id.ToString());

                //Order the matches by date
                var orderedMatches = matches.OrderByDescending(m => m.StartTime);

                MatchesListView.ItemsSource = orderedMatches;
            };
        }
    }
}