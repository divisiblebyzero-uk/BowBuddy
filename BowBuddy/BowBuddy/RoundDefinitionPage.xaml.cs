using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BowBuddy.Model;
using BowBuddy.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            HandicapCalculationService calc = new HandicapCalculationService();

            HandicapTable = calc.GetHandicapTable(round);

            HandicapList.ItemsSource = HandicapTable
                .OrderBy(h => h.handicap)
                .Select(h => new Tuple<int, int>(h.handicap, h.score))
                .ToList();



            
        }
    }
}
