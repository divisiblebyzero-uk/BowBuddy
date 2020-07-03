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

        protected override void OnAppearing()
        {
            base.OnAppearing();

           

            StringBuilder html = new StringBuilder();

            html.Append("<html>");
            html.Append("<style>table, th, td { border-collapse: collapse; border: 1px solid black;text-align: center;} th, td { padding: 3px; }</style>");
            html.Append("<body><table>");
            html.Append(
                "<tr><td colspan='6'>End 1 Scores</td><td>E/T</td><td colspan='6'>End 2</td><td>E/T</td><td>G</td><td>S</td><td>H</td><td>R/T</td></tr>");



            var scoreSheet = (ScoreSheet) BindingContext;
            new ScoreCalculationService().CalculateScores(scoreSheet);

            scoreSheet.Dozens.ForEach(dozen =>
                {
                    html.Append("<tr>");
                    dozen.Ends.ForEach(end =>
                    {
                        foreach (string score in end.Scores)
                        {
                            html.Append($"<td>{score}</td>");
                        }

                        html.Append($"<td>{end.EndTotal}</td>");
                    });
                    html.Append($"<td>{dozen.Total.Golds}</td>");
                    html.Append($"<td>{dozen.Total.Score}</td>");
                    html.Append($"<td>{dozen.Total.Hits}</td>");
                    html.Append($"<td>{dozen.Total.RunningTotal}</td>");
                    html.Append("</tr>");
                }
                );
            html.Append("<tr>");
            html.Append("<td colspan='14'>GrandTotal</td>");
            html.Append($"<td>{scoreSheet.Total.Golds}</td>");
            html.Append($"<td>{scoreSheet.Total.Score}</td>");
            html.Append($"<td>{scoreSheet.Total.Hits}</td>");
            html.Append($"<td>{scoreSheet.Total.RunningTotal}</td>");
            html.Append("</tr>");
            html.Append("</table></body></html>");

            webView.Source = new HtmlWebViewSource
            {
                Html = html.ToString()
            };

            if (scoreSheet.Handicap == 0)
            {
                NextHandicapScoreLabel.Text = "You already have the best handicap";
            }
            else
            {
                var hcs = new HandicapCalculationService();
                int nextScore = hcs.GetHandicapTable(RoundRegistry.Instance.Rounds[scoreSheet.RoundName])
                    .OrderByDescending(entry => entry.handicap)
                    .FirstOrDefault(entry => entry.handicap <= scoreSheet.Handicap).score;
                NextHandicapScoreLabel.Text = $"Score {nextScore} to reach the next handicap";
            }
        }
    }
}