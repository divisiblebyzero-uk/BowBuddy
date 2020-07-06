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
    public partial class ScoreSheetEntryHeaderPage : ContentPage
    {
        public ScoreSheetEntryHeaderPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            RoundPicker.ItemsSource = RoundRegistry.Instance.RoundNames;
            GenderPicker.ItemsSource = ScoreSheet.Genders;
            AgeGroupPicker.ItemsSource = ScoreSheet.AgeGroups;
            BowTypePicker.ItemsSource = ScoreSheet.BowTypes;
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bow Buddy", JsonConvert.SerializeObject(BindingContext), "OK");
        }

        private async void EndsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreSheetEntryPage
            {
                BindingContext = new ScoreSheetEntryEndsViewModel(((ScoreSheetEntryHeaderViewModel)BindingContext).ScoreSheet)
            });
        }
    }
}