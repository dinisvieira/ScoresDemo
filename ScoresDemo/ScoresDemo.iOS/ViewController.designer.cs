// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ScoresDemo.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIPickerView CompetitionPicker { get; set; }

		[Outlet]
		UIKit.UIButton FetchMatchesButton { get; set; }

		[Outlet]
		UIKit.UITableView MatchesTable { get; set; }

		[Action ("FetchMatchesButton_TouchUpInside:")]
		partial void FetchMatchesButton_TouchUpInside (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CompetitionPicker != null) {
				CompetitionPicker.Dispose ();
				CompetitionPicker = null;
			}

			if (FetchMatchesButton != null) {
				FetchMatchesButton.Dispose ();
				FetchMatchesButton = null;
			}

			if (MatchesTable != null) {
				MatchesTable.Dispose ();
				MatchesTable = null;
			}
		}
	}
}
