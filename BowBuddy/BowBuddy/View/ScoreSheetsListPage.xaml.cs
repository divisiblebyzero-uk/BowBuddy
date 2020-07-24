using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowBuddy.Model;
using BowBuddy.View;
using BowBuddy.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BowBuddy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreSheetsListPage : ContentPage
    {
        public ScoreSheetsListPage()
        {
            InitializeComponent();
        }

        private List<ScoreSheet> GetScoreSheets()
        {
            return new ScoreSheet[] 
            {
                new ScoreSheet
                {
                    Gender = ScoreSheet.GenderMale,
                    BowType = ScoreSheet.BowTypeRecurve,
                    AgeGroup = ScoreSheet.AgeGroupAdult,
                    Date = DateTime.Now,
                    Ends = Enumerable.Repeat(new End {Scores = new string[] {"X","9","8","7","M","M"}}, 10).ToList(),
                    RoundName = "Portsmouth"
                }
            }.ToList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var scoreSheets = GetScoreSheets();

            listView.ItemsSource = scoreSheets.OrderBy(s => s.Date).ToList();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ScoreSheetPage
                {
                    BindingContext = e.SelectedItem as ScoreSheet
                });
            }
        }

        async void OnScoreSheetAddedClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreSheetEntryHeaderPage
            {
                BindingContext = new ScoreSheetEntryHeaderViewModel()
            });

        }

        async void OnScoreSheetEditClick(object sender, EventArgs e)
        {
            var button = (Button) sender;
            var scoreSheet = (ScoreSheet)button.Parent.BindingContext;

            await Navigation.PushAsync(new ScoreSheetEntryHeaderPage
            {
                BindingContext = new ScoreSheetEntryHeaderViewModel(scoreSheet)
            });

        }
    }
}