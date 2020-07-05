using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowBuddy.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BowBuddy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundsListPage : ContentPage
    {
        public RoundsListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var rounds = RoundRegistry.Instance.Rounds.Values;

            listView.ItemsSource = rounds
                .OrderBy(r => r.Name)
                .ToList();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bow buddy", "This is an alert", "OK");
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new RoundDefinitionPage
                {
                    BindingContext = e.SelectedItem as Round
                });
            }
        }
    }
}