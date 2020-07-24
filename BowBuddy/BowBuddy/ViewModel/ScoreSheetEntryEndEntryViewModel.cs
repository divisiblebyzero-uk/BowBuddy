using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BowBuddy.Annotations;
using BowBuddy.Model;

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
        public readonly string Scoring;
        public readonly string[] Scores;

        public ScoreSheetEntryEndEntryViewModel()
        {

        }
        public ScoreSheetEntryEndEntryViewModel(ScoreSheet scoreSheet)
        {
            ScoreSheet = scoreSheet;
        }
    }
}
