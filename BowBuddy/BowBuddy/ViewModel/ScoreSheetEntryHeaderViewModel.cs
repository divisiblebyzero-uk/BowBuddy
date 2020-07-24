using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BowBuddy.Annotations;
using BowBuddy.Model;

namespace BowBuddy.ViewModel
{
    public class ScoreSheetEntryHeaderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public readonly ScoreSheet ScoreSheet;

        public IList<string> RoundNames()
        {
            return RoundRegistry.Instance.RoundNames;
        }

        public IList<string> Genders()
        {
            return ScoreSheet.Genders;
        }

        public IList<string> AgeGroups()
        {
            return ScoreSheet.AgeGroups;
        }

        public IList<string> BowTypes()
        {
            return ScoreSheet.BowTypes;
        }


        public DateTime Date
        {
            get { return ScoreSheet.Date; }
            set
            {
                ScoreSheet.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public string RoundName
        {
            get { return ScoreSheet.RoundName; }
            set
            {
                ScoreSheet.RoundName = value;
                OnPropertyChanged("RoundName");
            }
        }

        public string Gender
        {
            get { return ScoreSheet.Gender; }
            set
            {
                ScoreSheet.Gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public string AgeGroup
        {
            get { return ScoreSheet.AgeGroup; }
            set
            {
                ScoreSheet.AgeGroup = value;
                OnPropertyChanged("AgeGroup");
            }
        }

        public string BowType
        {
            get { return ScoreSheet.BowType; }
            set
            {
                ScoreSheet.BowType = value;
                OnPropertyChanged("BowType");
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScoreSheetEntryHeaderViewModel(ScoreSheet scoreSheet)
        {
            ScoreSheet = scoreSheet;
            //OnPropertyChanged("RoundName");
        }

        public ScoreSheetEntryHeaderViewModel()
        {
            ScoreSheet = new ScoreSheet();
        }

    }
}
