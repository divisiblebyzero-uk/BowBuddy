using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using BowBuddy.Annotations;
using BowBuddy.View;
using BowBuddy.ViewModel;
using Xamarin.Forms.Internals;

namespace BowBuddy.Model
{
    public class ScoreSheetEntryEndsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ScoreSheet ScoreSheet { get; }

        public string[] ScoreOptions { get; set; }

        private Round Round;

        public ICommand EditEnd { private set; get; }

        public INavigation Navigation;

        public ObservableCollection<DistanceViewModel> Distances { get; set; } =
            new ObservableCollection<DistanceViewModel>();

        public void AddEnd()
        {
            /*
            if (_scoreSheet.Dozens.Count == 0 || _scoreSheet.Dozens[_scoreSheet.Dozens.Count - 1].Ends.Count >= 2)
            {
                _scoreSheet.Dozens.Add(new Dozen());
            }
            _scoreSheet.Dozens[_scoreSheet.Dozens.Count - 1].Ends.Add(new End());
            */
            /*
            EndScores.Add(new ObservableCollection<string> {"X", "X", "7", "7", "5", "M"});
            EndsCount = EndScores.Count();
            // TODO Redo the ends in the backing model
            OnPropertyChanged("EndScores");
            OnPropertyChanged("EndsCount");
        */
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScoreSheetEntryEndsViewModel(INavigation navigation, ScoreSheet scoreSheet)
        {
            Navigation = navigation;
            ScoreSheet = scoreSheet;
            if (scoreSheet.RoundName != null && RoundRegistry.Instance.Rounds.ContainsKey(scoreSheet.RoundName))
            {
                Round = RoundRegistry.Instance.Rounds[scoreSheet.RoundName];
            }
            else
            {
                Round = RoundRegistry.Instance.Rounds["Junior National"];
            }

            ScoreOptions = RoundRegistry.Instance.ScoreOptions(Round);

            EditEnd = new Command<End>(async (End end) =>
            {
                var newModel = new ScoreSheetEntryEndEntryViewModel(ScoreSheet, end);

                await Navigation.PushAsync(new ScoreSheetEntryEndsEntryPage
                {
                    BindingContext = newModel
                });
            });
        }

        public void Reload()
        {
            Distances.Clear();
            int scoreSheetEndCount = ScoreSheet.Ends.Count;

            foreach (var distance in Round.Distances)
            {
                var dvm = new DistanceViewModel
                {
                    Distance = distance.DisplayString, Scores = new ObservableCollection<ObservableCollection<string>>()
                };
                for (int i = 0; i < distance.Arrows / 6 && i < scoreSheetEndCount; i++)
                {
                    dvm.Scores.Add(new ObservableCollection<string>(ScoreSheet.Ends[i].Scores));
                }

                Distances.Add(dvm);
            }

            OnPropertyChanged("Distances");
            OnPropertyChanged("ScoreSheet");
            OnPropertyChanged("ScoreOptions");
        }
    }

    public class DistanceViewModel
    {
        public string Distance { get; set; }
        public ObservableCollection<ObservableCollection<string>> Scores { get; set; }
    }
}
