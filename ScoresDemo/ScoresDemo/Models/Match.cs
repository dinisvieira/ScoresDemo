﻿using System;

namespace ScoresDemo
{
    public class Match
    {
        public string HomeName { get; set; }
        public string AwayName { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public string VenueName { get; set; }
        public string HomeShirtImgUrl { get; set; }
        public string AwayShirtImgUrl { get; set; }
        public DateTime StartTime { get; set; }
    }
}
