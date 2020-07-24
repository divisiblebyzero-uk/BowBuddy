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
            
            var viewModel = (ScoreSheetEntryHeaderViewModel) BindingContext;

            var currentRoundName = viewModel.RoundName;

            RoundPicker.ItemsSource = RoundRegistry.Instance.RoundNames;
            RoundPicker.SetBinding(Picker.SelectedItemProperty, new Binding("RoundName"));
            viewModel.RoundName = currentRoundName;

            GenderPicker.ItemsSource = ScoreSheet.Genders;
            GenderPicker.SetBinding(Picker.SelectedItemProperty, new Binding("Gender"));
            

            AgeGroupPicker.ItemsSource = ScoreSheet.AgeGroups;
            AgeGroupPicker.SetBinding(Picker.SelectedItemProperty, new Binding("AgeGroup"));

            BowTypePicker.ItemsSource = ScoreSheet.BowTypes;
            BowTypePicker.SetBinding(Picker.SelectedItemProperty, new Binding("BowType"));
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