using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowBuddy.Model;
using BowBuddy.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BowBuddy.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreSheetEntryEndsEntryPage : ContentPage
    {
        public ScoreSheetEntryEndsEntryPage()
        {
            InitializeComponent();
        }

        public void AddScoresToSheet()
        {
            var viewModel = (ScoreSheetEntryEndEntryViewModel)BindingContext;
            viewModel.ScoreSheet.Ends.Add(new End { Scores = viewModel.Scores });

        }
    }
}