using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowBuddy.Model;
using Newtonsoft.Json;

namespace BowBuddy.Service
{
    /**
     * Interpreted from: http://www.roystonarchery.org/new/wp-content/uploads/2013/09/Graduated-Handicap-Tables.pdf?e632e9
     */
    public class HandicapCalculationService
    {

        private static readonly Lazy<HandicapCalculationService> instance = new Lazy<HandicapCalculationService>(() => new HandicapCalculationService());
        public static HandicapCalculationService Instance => instance.Value;

        private HandicapCalculationService()
        {

        }

        private Dictionary<string, List<(int, int)>> HandicapTables = new Dictionary<string, List<(int, int)>>();

        public int CalculateHandicap(Round round, int score)
        {
            List<(int handicap, int score)> handicapTable = GetHandicapTable(round);
            var value = handicapTable.FirstOrDefault(h => h.score <= score);
            if (value.handicap == 0 && value.score == 0)
            {
                return 100;
            }
            else
            {
                return value.handicap;
            }

        }

        public List<(int handicap, int score)> GetHandicapTable(Round round)
        {
            if (HandicapTables.ContainsKey(round.Name))
            {
                return HandicapTables[round.Name];
            }

            List<(int handicap, int score)> handicapTable = new List<(int, int)>();
            for (int i = 0; i < 101; i++)
            {
                handicapTable.Add((i, CalculateScoreForHandicap(i, round)));
            }

            HandicapTables[round.Name] = handicapTable;
            return handicapTable;
        }

        private int CalculateScoreForHandicap(int handicap, Round round)
        {
            double runningTotal = 0d;
            foreach (RoundDistance distance in round.Distances)
            {
                runningTotal += distance.Arrows * CalculateScoreForHandicap(handicap, distance, round.Scoring);
            }
            return Convert.ToInt32(runningTotal);
        }

        private double CalculateScoreForHandicap(int handicap, RoundDistance distance, string scoring)
        {
            double d = 0.0D;
            double rootMeanSquare = GetRootMeanSquare(handicap, distance);
            double averageScore = d;

            if (scoring == Round.ScoringStyleImperial)
            {
                for (int i = 1; i <= 4; i++)
                {
                    
                    averageScore += Math.Exp(-Math.Pow(i * distance.FaceSize.InCentimetres() / 10.0D + 0.357D, 2.0D) / Math.Pow(rootMeanSquare, 2.0D));
                }
                return 9.0D - 2.0D * averageScore - Math.Exp(-Math.Pow(distance.FaceSize.InCentimetres() / 2.0D + 0.357D, 2.0D) / Math.Pow(rootMeanSquare, 2.0D));
            }
            else if (scoring == Round.ScoringStyleMetric)
            {
                for (int i = 1; i <= 10; i++)
                {
                    averageScore += Math.Exp(-Math.Pow(i * distance.FaceSize.InCentimetres() / 20.0D + 0.357D, 2.0D) / Math.Pow(rootMeanSquare, 2.0D));
                }
                return 10.0D - averageScore;
            }
            else if (scoring == Round.ScoringStyleWorcester)
            {
                for (int i = 1; i <= 5; i++)
                {
                    averageScore += Math.Exp(-Math.Pow(i * distance.FaceSize.InCentimetres() / 10.0D + 0.357D, 2.0D) / Math.Pow(rootMeanSquare, 2.0D));
                }
                return 5.0D - averageScore;
            }
            else
            {
                
                return 0d;
            }
        }

        private double GetRootMeanSquare(int handicap, RoundDistance distance)
        {
            double rangeInMetres = distance.Distance.InMetres();
            return 100.0D * rangeInMetres * Math.Pow(1.036D, 12.9D + handicap) * 5.0D * Math.Pow(10.0D, -4.0D) * (1.0D + 1.429D * Math.Pow(10.0D, -6.0D) * Math.Pow(1.07D, 4.3D + handicap) * Math.Pow(rangeInMetres, 2.0D));
        }

        public string GetHandicapChartHtml(Round round)
        {
            return GetHandicapChartHtml(GetHandicapTable(round));
        }

        public string GetHandicapChartHtml(List<(int handicap, int score)> HandicapTable)
        {
            var labels = JsonConvert.SerializeObject(Enumerable.Range(0, 101).OrderByDescending(i => i).ToArray());
            var data = JsonConvert.SerializeObject(HandicapTable.OrderByDescending(h => h.handicap).Select(h => h.score).ToArray());

            StringBuilder html = new StringBuilder();
            html.Append("<html>");
            html.Append("	<head><script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.bundle.min.js'></script></head>");
            html.Append("	<body>");
            html.Append("		<div style='height:100%'>");
            html.Append("			<canvas id='chart'/>");
            html.Append("		</div>");
            html.Append("		<script>");
            html.Append("			var config = {");
            html.Append("				type: 'line',");
            html.Append("				options: {");
            html.Append("                   legend: { display: false },");
            html.Append("                   scales: { xAxes: [{ max: 0, min: 100, stepSize: -10, maxTicksLimit: 10 }] },");
            html.Append("					responsive: true,");
            html.Append("					maintainAspectRatio: false,");
            html.Append("					legend: {");
            html.Append("						position: 'top'");
            html.Append("					},");
            html.Append("					animation: {");
            html.Append("						animateScale: true");
            html.Append("					}");
            html.Append("				},");
            html.Append("				data: {");


            html.Append($"					labels: {labels},");
            html.Append("					datasets: [{");
            html.Append("						label: 'Score',");
            html.Append("						backgroundColor: '#ffcccc',");
            html.Append("						borderColor: '#ff0000',");

            html.Append($"						data: {data},");
            html.Append("						fill: true,");
            html.Append("						pointRadius: 1");
            html.Append("					}]");
            html.Append("");
            html.Append("				}");
            html.Append("			};");
            html.Append("			window.onload = function() {{");
            html.Append("			  var canvasContext = document.getElementById('chart').getContext('2d');");
            html.Append("			  new Chart(canvasContext, config);");
            html.Append("			}};");
            html.Append("		</script>");
            html.Append("	</body>");
            html.Append("</html>");

            return html.ToString();
        }

    }
}
