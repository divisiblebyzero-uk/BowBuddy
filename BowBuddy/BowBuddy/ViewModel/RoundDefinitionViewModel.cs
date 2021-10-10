using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using BowBuddy.Annotations;
using BowBuddy.Model;
using BowBuddy.Service;
using Xamarin.Forms;

namespace BowBuddy.ViewModel
{
    public class RoundDefinitionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Round Round { get; }
        public int ClassificationScore3rd { get; }
        public int ClassificationScore2nd { get; }
        public int ClassificationScore1st { get; }
        public int ClassificationScoreBowman { get; }
        private HandicapCalculationService Hcs = HandicapCalculationService.Instance;

        public RoundDefinitionViewModel()
        {

        }

        private int GetScoreForHandicap(List<(int handicap, int score)> handicapTable, int handicap)
        {
            var index = handicapTable.FindIndex(h => h.handicap == handicap);
            return handicapTable[index].score;
        }

        public RoundDefinitionViewModel(Round round)
        {
            Round = round;
            var handicaps = Hcs.GetHandicapTable(round);
            ClassificationScoreBowman = GetScoreForHandicap(handicaps, 36);
            ClassificationScore1st = GetScoreForHandicap(handicaps, 44);
            ClassificationScore2nd = GetScoreForHandicap(handicaps, 50);
            ClassificationScore3rd = GetScoreForHandicap(handicaps, 57);
        }
    }
}
