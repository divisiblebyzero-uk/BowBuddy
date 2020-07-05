using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.Forms;
using BowBuddy.Annotations;

namespace BowBuddy.Model
{
    public class ScoreSheetEntryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public readonly ScoreSheet _scoreSheet;

        public IList<string> RoundNames => RoundRegistry.Instance.RoundNames;  
        public IList<string> Genders => ScoreSheet.Genders;
        public IList<string> AgeGroups => ScoreSheet.AgeGroups;
        public IList<string> BowTypes => ScoreSheet.BowTypes;


        public DateTime Date
        {
            get { return _scoreSheet.Date; }
            set
            {
                _scoreSheet.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public string RoundName
        {
            get { return _scoreSheet.RoundName;  }
            set
            {
                _scoreSheet.RoundName = value;
                OnPropertyChanged("RoundName");
            }
        }

        public string Gender
        {
            get { return _scoreSheet.Gender; }
            set
            {
                _scoreSheet.Gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public string AgeGroup
        {
            get { return _scoreSheet.AgeGroup; }
            set
            {
                _scoreSheet.AgeGroup = value;
                OnPropertyChanged("AgeGroup");
            }
        }

        public string BowType
        {
            get { return _scoreSheet.BowType; }
            set
            {
                _scoreSheet.BowType = value;
                OnPropertyChanged("BowType");
            }
        }

        public IEnumerable<End> Ends
        {
            get { return _scoreSheet.Dozens.SelectMany(d => Ends); }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScoreSheetEntryViewModel(ScoreSheet scoreSheet)
        {
            _scoreSheet = scoreSheet;
        }

        public ScoreSheetEntryViewModel()
        {
            _scoreSheet = new ScoreSheet();
        }

    }
}
