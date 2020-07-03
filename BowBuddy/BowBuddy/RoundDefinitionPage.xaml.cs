using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BowBuddy.Model;
using BowBuddy.Service;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace BowBuddy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundDefinitionPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public List<(int handicap, int score)> HandicapTable;

        public RoundDefinitionPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var round = (Round)BindingContext;

            HandicapCalculationService calc = HandicapCalculationService.Instance;

            HandicapTable = calc.GetHandicapTable(round);

            /*
            HandicapList.ItemsSource = HandicapTable
                .OrderBy(h => h.handicap)
                .Select(h => new Tuple<int, int>(h.handicap, h.score))
                .ToList();
                */


            chartView.Chart = new LineChart()
            {
                Entries = GetEntries(), 
                LineSize = 1, 
                LineMode = LineMode.Straight,
                LabelTextSize = 32
            };

        }


        public Entry[] GetEntries()
        {
            return HandicapTable.GroupBy(x => ((int) x.handicap) / 10).Select(x => new Entry(x.First().score)
            {
                Label = x.First().handicap.ToString(),
                ValueLabel = x.First().score.ToString()
            }).ToArray();

            /*
            List<Entry> entries = new List<Entry>();

            foreach ((int handicap, int score) in HandicapTable)
            {
                entries.Add(new Entry(score){ValueLabel=$"{handicap}"});
            }

            return entries.ToArray();
            */
            /*
            return new Entry[]
            {
                new Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#266489")
                },
                new Entry(400)
                {
                    Label = "February",
                    ValueLabel = "400",
                    Color = SKColor.Parse("#68B9C0")
                },
                new Entry(-100)
                {
                    Label = "March",
                    ValueLabel = "-100",
                    Color = SKColor.Parse("#90D585")
                }
            };
        */
        }
    }
}
