using Foundation;
using System;
using UIKit;

namespace ScoresDemo.iOS
{
    public partial class MatchTableViewCell : UITableViewCell
    {
        public UILabel HomeNameProp { get { return HomeName; } }

        public UILabel AwayNameProp { get { return AwayName; } }

        public UILabel HomeGoalsProp { get { return HomeGoals; } }

        public UILabel AwayGoalsProp { get { return AwayGoals; } }

        public MatchTableViewCell()
        {
        }

        public MatchTableViewCell (IntPtr handle) : base (handle)
        {
        }
    }
}