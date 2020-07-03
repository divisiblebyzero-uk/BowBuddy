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

        private string GetColourForScore(string score)
        {
            if (String.IsNullOrEmpty(score))
            {
                return "white";
            }

            switch (score)
            {
                case "X":
                    return "gold";
                case "10":
                    return "gold";
                case "9":
                    return "gold";
                case "8":
                    return "red";
                case "7":
                    return "red";
                case "6":
                    return "blue";
                case "5":
                    return "blue";
                case "4":
                    return "black";
                case "3":
                    return "black";
                default:
                    return "white";
            }
        }

        private string GetScoreSheetHtml(ScoreSheet scoreSheet)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<html>");
            html.Append("	<style>");
            html.Append("		table, th, td {");
            html.Append("			border-collapse: collapse; border: 1px solid black; text-align: center;");
            html.Append("		}");
            html.Append("		th, td {");
            html.Append("			font-family: sans-serif;");
            html.Append("			padding: 2px;");
            html.Append("		}");
            html.Append("		.circle {");
            html.Append("			padding: 1px 3px;");
            html.Append("			display: inline-block;");
            html.Append("			border-radius: 16px;");
            html.Append("			font-weight: bold;");
            html.Append("		}");
            html.Append("		.gold-circle {");
            html.Append("			color: black;");
            html.Append("			background-color: #ffff00;");
            html.Append("		}");
            html.Append("		.red-circle {");
            html.Append("			color: black;");
            html.Append("			background-color: #ff0000;");
            html.Append("		}");
            html.Append("		.blue-circle {");
            html.Append("			color: white;");
            html.Append("			background-color: #0004ff;");
            html.Append("		}");
            html.Append("		.black-circle {");
            html.Append("			color: white;");
            html.Append("			background-color: #000000;");
            html.Append("		}");
            html.Append("		.white-circle {");
            html.Append("			color: black;");
            html.Append("			background-color: #ffffff;");
            html.Append("		}");
            html.Append("	</style>");
            html.Append("	<body>");
            html.Append("		<table>");
            html.Append("			<tr>");
            html.Append("				<td colspan='6'>End 1</td>");
            html.Append("				<td>E/T</td>");
            html.Append("				<td colspan='6'>End 2</td>");
            html.Append("				<td>E/T</td>");
            html.Append("				<td>Golds</td>");
            html.Append("				<td>Score</td>");
            html.Append("				<td>Hits</td>");
            html.Append("				<td>R/T</td>");
            html.Append("			</tr>");

            scoreSheet.Dozens.ForEach(dozen =>
                {
                    html.Append("<tr>");
                    dozen.Ends.ForEach(end =>
                    {
                        foreach (string score in end.Scores)
                        {
                            string colour = GetColourForScore(score);
                            html.Append($"<td><span class='circle {colour}-circle'>{score}</span></td>");
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
            return html.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            var scoreSheet = (ScoreSheet)BindingContext;
            new ScoreCalculationService().CalculateScores(scoreSheet);

            webView.Source = new HtmlWebViewSource
            {
                Html = GetScoreSheetHtml(scoreSheet)
            };

            HandicapLabel.Text = $"Handicap: {scoreSheet.Handicap}";

            if (scoreSheet.Handicap == 0)
            {
                NextHandicapScoreLabel.Text = "You already have the best handicap";
            }
            else
            {
                var hcs = HandicapCalculationService.Instance;
                int nextScore = hcs.GetHandicapTable(RoundRegistry.Instance.Rounds[scoreSheet.RoundName])
                    .OrderByDescending(entry => entry.handicap)
                    .FirstOrDefault(entry => entry.handicap < scoreSheet.Handicap).score;
                NextHandicapScoreLabel.Text = $"Score {nextScore} to reach the next handicap";
            }
        }
    }
}