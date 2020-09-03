using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using BowBuddy.Annotations;
using BowBuddy.Model;
using Xamarin.Forms;

namespace BowBuddy.ViewModel
{
    public class ScoreSheetEntryEndEntryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public readonly ScoreSheet ScoreSheet;
        public readonly End End;
        public string[] ScoreOptions { get; set; }
        public readonly string[] Scores;
        private readonly Round Round;

        public string[] NewEndScores { get; set; } = {"X", "X", "X", "X", "X", "X"};

        public ICommand SaveCommand { private set; get; }

        public ScoreSheetEntryEndEntryViewModel()
        {

        }
        public ScoreSheetEntryEndEntryViewModel(ScoreSheet scoreSheet, End end)
        {
            ScoreSheet = scoreSheet;
            End = end;
            if (scoreSheet.RoundName != null && RoundRegistry.Instance.Rounds.ContainsKey(scoreSheet.RoundName))
            {
                Round = RoundRegistry.Instance.Rounds[scoreSheet.RoundName];
            }
            else
            {
                Round = RoundRegistry.Instance.Rounds["Junior National"];
            }

            ScoreOptions = RoundRegistry.Instance.ScoreOptions(Round);

            SaveCommand = new Command(
                execute: () => { end.Scores = NewEndScores; });
        }
    }
}
