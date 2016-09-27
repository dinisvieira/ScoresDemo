// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ScoresDemo.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView CompetitionPicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton FetchMatchesButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView MatchesTable { get; set; }

        [Action ("FetchMatchesButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
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