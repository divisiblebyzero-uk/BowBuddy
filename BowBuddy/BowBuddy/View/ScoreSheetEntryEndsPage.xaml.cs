using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bow Buddy", JsonConvert.SerializeObject(BindingContext), "OK");
        }

        private async void AddEndClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreSheetEntryEndsEntryPage
            {
                BindingContext =
                    new ScoreSheetEntryEndEntryViewModel(((ScoreSheetEntryEndsViewModel) BindingContext).ScoreSheet)
            });
        }
    }


}