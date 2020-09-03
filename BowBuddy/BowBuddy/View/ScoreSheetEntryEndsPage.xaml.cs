using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BowBuddy.Annotations;
using BowBuddy.Model;
using BowBuddy.View;
using BowBuddy.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BowBuddy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreSheetEntryPage : ContentPage
    {

        
        public ScoreSheetEntryPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = (ScoreSheetEntryEndsViewModel) BindingContext;
            viewModel.Reload();
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bow Buddy", JsonConvert.SerializeObject(BindingContext), "OK");
        }

        private async void AddEndClicked(object sender, EventArgs e)
        {
            var newEnd = new End {Scores = new string[] {null, null, null, null, null, null}};
            var viewModel = (ScoreSheetEntryEndsViewModel) BindingContext;
            viewModel.ScoreSheet.Ends.Add(newEnd);
            var newModel = new ScoreSheetEntryEndEntryViewModel(viewModel.ScoreSheet, newEnd);
                
            await Navigation.PushAsync(new ScoreSheetEntryEndsEntryPage
            {
                BindingContext = newModel
            });
        }

        private async void EditEndClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bow Buddy", JsonConvert.SerializeObject(e), "OK");
        }
    }


}