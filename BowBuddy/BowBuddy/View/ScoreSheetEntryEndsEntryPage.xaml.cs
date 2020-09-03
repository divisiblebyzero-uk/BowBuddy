using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowBuddy.Model;
using BowBuddy.ViewModel;
using Newtonsoft.Json;
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
        public async void ShowModelButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bow Buddy", JsonConvert.SerializeObject(BindingContext), "OK");
        }

        public void AddScoresToSheet()
        {
            var viewModel = (ScoreSheetEntryEndEntryViewModel)BindingContext;
            viewModel.ScoreSheet.Ends.Add(new End { Scores = viewModel.Scores });

        }
    }
}