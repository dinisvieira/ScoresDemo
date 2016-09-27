using System;
using System.Collections.ObjectModel;
using Foundation;
using UIKit;

namespace ScoresDemo.iOS
{
	public partial class ViewController : UIViewController, IUITableViewDataSource//, IUITableViewDelegate
	{
	    private ObservableCollection<Match> matchesCollection;

        public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            //Calls the Shared Portable Class Library to get a list with all available competitions.
            ObservableCollection<Competition> memes = await ScoresSource.GetCompetitions();

            //Set the list of competitions we got to our PickerView (using the pickerViewModel we created)
            CompetitionPicker.Model = new CompetitionsPickerViewModel(memes);

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
            var competionIdString = (CompetitionPicker.Model as CompetitionsPickerViewModel).GetId(rowSel);

            //Calls the Shared Portable Class Library with the values of the PickerView and TextFields’s in this View.
            //The returned value is the image in a byte array format 
            matchesCollection = await ScoresSource.GetLastCompetitionMatches(competionIdString);
            //Create image
            //var img = new UIImage(NSData.FromArray(imageByteArr));

            ////Set image to the Image Placeholder we have on our View
            //MemeImageView.Image = img;
        }

	    public nint RowsInSection(UITableView tableView, nint section)
	    {
	        if (matchesCollection != null)
	        {
	            return matchesCollection.Count;
	        }
	        return 0;
	    }

	    public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
	    {
            // request a recycled cell to save memory
            UITableViewCell cell = tableView.DequeueReusableCell("cellxpto");
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, "cellxpto");
            cell.TextLabel.Text = matchesCollection[indexPath.Row].HomeName;
            return cell;
        }
	}

    public class CompetitionsPickerViewModel : UIPickerViewModel
    {

        private Competition[] _compArr;

        public CompetitionsPickerViewModel(ObservableCollection<Competition> competitions)
        {
            _compArr = new Competition[competitions.Count];
            competitions.CopyTo(_compArr, 0);
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