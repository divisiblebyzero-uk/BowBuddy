using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowBuddy.Model;
using BowBuddy.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BowBuddy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreSheetPage : ContentPage
    {
        public ScoreSheetPage()
        {
            InitializeComponent();
        }

        protected void AddChildToGrid(string text, int rowNumber, int colNumber)
        {
            ScoresGrid.Children.Add(new Label { Text = text }, rowNumber= rowNumber, colNumber = colNumber);
        }

        protected void AddChildToGrid(int value, int rowNumber, int colNumber)
        {
            AddChildToGrid(value.ToString(), rowNumber, colNumber);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var scoreSheet = (ScoreSheet) BindingContext;
            new ScoreCalculationService().CalculateScores(scoreSheet);
            var rowNumber = 1;
            scoreSheet.Dozens.ForEach(dozen =>
                {
                    var colNumber = 0;
                    dozen.Ends.ForEach(end =>
                    {
                        foreach (string score in end.Scores)
                        {
                            AddChildToGrid(score, colNumber++, rowNumber);
                        }
                        AddChildToGrid(end.EndTotal, colNumber++, rowNumber);
                    });
                    AddChildToGrid(dozen.Total.Golds, colNumber++, rowNumber);
                    AddChildToGrid(dozen.Total.Score, colNumber++, rowNumber);
                    AddChildToGrid(dozen.Total.Hits, colNumber++, rowNumber);
                    AddChildToGrid(dozen.Total.RunningTotal, colNumber++, rowNumber);
                    rowNumber++;
                }
                );
        }
    }
}