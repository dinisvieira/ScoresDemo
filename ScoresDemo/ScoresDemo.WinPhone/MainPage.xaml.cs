using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ScoresDemo.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int count = 1;

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
            //Calls the Shared Portable Class Library to get a list with all available meme's.
            ObservableCollection<Competition> competitions = await ScoresSource.GetCompetitions();

            var orderedCompetitions = competitions.OrderBy(m => m.Id);

            CompetitionsComboBox.ItemsSource = orderedCompetitions;

            FetchMatchesButton.Tapped += async (sender, args) =>
            {

                var selComp = CompetitionsComboBox.SelectedValue as Competition;

                var matches = await ScoresSource.GetLastCompetitionMatches(selComp.Id.ToString());
                System.Diagnostics.Debug.WriteLine(matches.Count);

                var orderedMatches = matches.OrderByDescending(m => m.StartTime);

                MatchesListView.ItemsSource = orderedMatches;
            };
        }
    }
}
