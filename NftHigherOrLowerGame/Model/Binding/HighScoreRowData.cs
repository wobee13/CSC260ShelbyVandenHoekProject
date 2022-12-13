﻿using NftHigherOrLowerGame.Model.DataBaseModels;
using System.ComponentModel;

namespace NftHigherOrLowerGame.Model.Binding
{
    public class HighScoreRowData
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        [DisplayName("Rounds Survived")]
        public int TotalRounds { get; set; }
        public string Mode { get; set; }
        public string Date { get; set; }

        public HighScoreRowData(HighScore hs)
        {
            Name = hs.Name;
            Score = hs.Points;
            Correct = hs.Correct;
            Wrong = hs.Wrong;
            TotalRounds = hs.Total;
            Mode = hs.Mode;
            Date = DateTime.Parse(hs.CreatedAt).ToShortDateString();
        }
    }
}
