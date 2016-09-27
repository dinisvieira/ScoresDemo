using System;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using UIKit;

namespace ScoresDemo.iOS
{
	public partial class ViewController : UIViewController, IUITableViewDataSource//, IUITableViewDelegate
	{
	    private Match[] matchesArray;

        public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            //Calls the Shared Portable Class Library to get a list with all available competitions.
            ObservableCollection<Competition> competitions = await ScoresSource.GetCompetitions();

            //order the competitions by id
            var orderedCompetitionsArr = competitions.OrderBy(m => m.Id).ToArray();

            //Set the list of competitions we got to our PickerView (using the pickerViewModel we created)
            CompetitionPicker.Model = new CompetitionsPickerViewModel(orderedCompetitionsArr);

            //MatchesTable.Delegate = this;
            MatchesTable.DataSource = this;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

        async partial void FetchMatchesButton_TouchUpInside(UIButton sender)
        {
            //Get current selected meme from the ViewPicker
            var rowSel = CompetitionPicker.SelectedRowInComponent(new nint(0));
            var competitionIdString = (CompetitionPicker.Model as CompetitionsPickerViewModel).GetId(rowSel);

            //Calls the PCL with the chosen competition Id to get the matches List
            var matchesCollection = await ScoresSource.GetLastCompetitionMatches(competitionIdString);

            //Order the matches by date
            matchesArray = matchesCollection.OrderByDescending(m => m.StartTime).ToArray();

            //Reload the tableview
            MatchesTable.ReloadData();
        }

	    public nint RowsInSection(UITableView tableView, nint section)
	    {
	        if (matchesArray != null)
	        {
	            return matchesArray.Length;
	        }
	        return 0;
	    }

	    public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
	    {
            // request a recycled cell to save memory
            MatchTableViewCell cell = tableView.DequeueReusableCell("MatchCell") as MatchTableViewCell;

            // if there are no cells to reuse, create a new one
            if (cell == null) { 
                cell = new MatchTableViewCell();
            }

            cell.HomeNameProp.Text = matchesArray[indexPath.Row].HomeName;
            cell.AwayNameProp.Text = matchesArray[indexPath.Row].AwayName;
            cell.HomeGoalsProp.Text = matchesArray[indexPath.Row].HomeGoals.ToString();
            cell.AwayGoalsProp.Text = matchesArray[indexPath.Row].AwayGoals.ToString();
            return cell;
        }
	}

    public class CompetitionsPickerViewModel : UIPickerViewModel
    {

        private Competition[] _compArr;

        public CompetitionsPickerViewModel(Competition[] competitions)
        {
            _compArr = competitions;
        }

        public string GetId(nint row)
        {
            return _compArr[row].Id.ToString();
        }

        public string GetTitle(nint row)
        {
            return _compArr[row].Name;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _compArr[row].Name;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _compArr.Length;
        }
    }
}