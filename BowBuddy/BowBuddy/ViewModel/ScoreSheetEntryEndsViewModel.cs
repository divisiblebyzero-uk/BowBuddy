using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.Forms;
using BowBuddy.Annotations;
using Xamarin.Forms.Internals;

namespace BowBuddy.Model
{
    public class ScoreSheetEntryEndsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public readonly ScoreSheet ScoreSheet;

        public ObservableCollection<ObservableCollection<string>> EndScores { get; set; } = new ObservableCollection<ObservableCollection<string>>();
        public int EndsCount { get; set; } = 0;
        public string EndsCountString { get; set; } = "hello";

        public void AddEnd()
        {
            /*
            if (_scoreSheet.Dozens.Count == 0 || _scoreSheet.Dozens[_scoreSheet.Dozens.Count - 1].Ends.Count >= 2)
            {
                _scoreSheet.Dozens.Add(new Dozen());
            }
            _scoreSheet.Dozens[_scoreSheet.Dozens.Count - 1].Ends.Add(new End());
            */
            EndScores.Add(new ObservableCollection<string> {"X", "X", "7", "7", "5", "M"});
            EndsCount = EndScores.Count();
            // TODO Redo the ends in the backing model
            OnPropertyChanged("EndScores");
            OnPropertyChanged("EndsCount");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScoreSheetEntryEndsViewModel(ScoreSheet scoreSheet)
        {
            ScoreSheet = scoreSheet;
            Enumerable.Range(0, 12).ForEach(i => EndScores.Add(new ObservableCollection<string> { "X", "X", "7", "7", "5", "M" }));
            /*
            _scoreSheet.Dozens.ForEach(dozen => dozen.Ends.ForEach(end => Ends.Add(end)));
        */
            EndsCount = EndScores.Count();
        }

        public ScoreSheetEntryEndsViewModel()
        {
            ScoreSheet = new ScoreSheet();
        }

    }
}
