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

        public ObservableCollection<DistanceViewModel> Distances { get; set; } =
            new ObservableCollection<DistanceViewModel>();


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

            var distance1 = new DistanceViewModel { Distance = "40 yds", Scores = new ObservableCollection<ObservableCollection<string>>() };
            Enumerable.Range(0, 8).ForEach(i => distance1.Scores.Add(new ObservableCollection<string> { "X", "X", "7", "7", "5", "M" }));
            var distance2 = new DistanceViewModel { Distance = "30 yds", Scores = new ObservableCollection<ObservableCollection<string>>() };
            Enumerable.Range(0, 4).ForEach(i => distance2.Scores.Add(new ObservableCollection<string> { "X", "X", "7", "7", "5", "M" }));
            Distances.Add(distance1);
            Distances.Add(distance2);

            OnPropertyChanged("Distances");
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

    public class DistanceViewModel
    {
        public string Distance { get; set; }
        public ObservableCollection<ObservableCollection<string>> Scores { get; set; }
    }
}
