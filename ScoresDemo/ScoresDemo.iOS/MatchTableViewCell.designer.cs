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
    [Register ("MatchTableViewCell")]
    partial class MatchTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AwayGoals { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AwayName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HomeGoals { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HomeName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AwayGoals != null) {
                AwayGoals.Dispose ();
                AwayGoals = null;
            }

            if (AwayName != null) {
                AwayName.Dispose ();
                AwayName = null;
            }

            if (HomeGoals != null) {
                HomeGoals.Dispose ();
                HomeGoals = null;
            }

            if (HomeName != null) {
                HomeName.Dispose ();
                HomeName = null;
            }
        }
    }
}